using System.Text.Encodings.Web;
using System.Text.Json;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;
using SpellCardsGenerator.Runner.WebScraper.Models;

namespace SpellCardsGenerator.Runner.WebScraper;

public static class Scraper
{
  private const SupportedBrowser DefaultBrowser = SupportedBrowser.Chrome;
  private static readonly Uri baseUrl = new("http://dnd5e.wikidot.com");
  private static readonly JsonSerializerOptions SerializerOptions = new()
  {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };
  private static readonly string PuppeteerCacheDir = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
    "PuppeteerCache"
  );

  public static async Task Start()
  {
    BrowserFetcher browserFetcher = new(DefaultBrowser) { CacheDir = PuppeteerCacheDir };

    InstalledBrowser installedBrowser = browserFetcher.GetInstalledBrowsers()
      .First(browser => browser.Browser == DefaultBrowser);

    IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions()
    {
      ExecutablePath = installedBrowser.GetExecutablePath(),
      Browser = DefaultBrowser,
      Headless = true
    });

    string scriptsDir = Path.Combine(AppContext.BaseDirectory, "Scripts");
    string getSpellsScript = await File.ReadAllTextAsync(Path.Combine(scriptsDir, "getSpells.js"));
    string getSpellAdditionalDataScript = await File.ReadAllTextAsync(Path.Combine(scriptsDir, "getSpellAdditionalData.js"));

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Going to site...");
    await using IPage mainPage = await browser.NewPageAsync();
    await mainPage.GoToAsync(new Uri(baseUrl, "/spells").ToString());
    Console.WriteLine("There");

    Spell[] spells = await mainPage.EvaluateExpressionAsync<Spell[]>(getSpellsScript);
    Console.WriteLine($"Retrieved all spells. ({spells.Length})");
    
    string oldSpellDatasStr = await File.ReadAllTextAsync(@"C:\Repos\SpellCardsGenerator\helpers\Web\OldSpellsData-html.json");
    SpellCombined[] oldSpellDatas = JsonSerializer.Deserialize<SpellCombined[]>(oldSpellDatasStr) ?? throw new Exception("kurcze");
    HashSet<string> oldSpellLinks = oldSpellDatas
      .Select(x => x.Link)
      .ToHashSet();

    spells = spells
      .Where(spell => !oldSpellLinks.Contains(spell.Link))
      .ToArray();
    
    Dictionary<string, Spell> spellsDict = spells.ToDictionary(x => x.Link);
    Dictionary<string, SpellData> spellDatasDict = new(10);
    List<Spell> failedSpells = new();

    await using IPage spellPage = await browser.NewPageAsync();
    for (var i = 0; i < spells.Length; i++)
    {
      Spell spell = spells[i];
      try
      {
        await spellPage.GoToAsync(new Uri(baseUrl, spell.Link).ToString());
        SpellData spellData = await spellPage.EvaluateExpressionAsync<SpellData>(getSpellAdditionalDataScript);

        spellDatasDict.Add(spell.Link, spellData);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"({(i + 1).ToString().PadLeft(3, '0')}/{spells.Length}). Retrieved spell '{spell.Name}'.");
      }
      catch (Exception e)
      {
        spell.Components = "EXCEPTION: " + e.Message;
        failedSpells.Add(spell);

        await WriteFailed();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"({(i + 1).ToString().PadLeft(3, '0')}/{spells.Length}). FAILURE '{spell.Name}'.");
      }

      if (i % 10 == 0)
      {
        await WriteSpells();
      }
    }

    Console.ForegroundColor = ConsoleColor.White;

    await WriteSpells();
    await WriteFailed();
  
    return;
    
    
    async Task WriteFailed()
    {
      string serializedSpellsData = JsonSerializer.Serialize(failedSpells, SerializerOptions).Replace('’', '\'');
      await File.WriteAllTextAsync(@"C:\Repos\SpellCardsGenerator\helpers\Web\SpellsData-failed.json",
        serializedSpellsData);
    }

    async Task WriteSpells()
    {
      SpellCombined[] combinedSpells = spellDatasDict
        .Select(pair =>
        {
          Spell spell = spellsDict[pair.Key];
          return Convert(spell, pair.Value);
        })
        .ToArray();

      string serializedSpellsData = JsonSerializer.Serialize(combinedSpells, SerializerOptions).Replace('’', '\'');
      await File.WriteAllTextAsync(@"C:\Repos\SpellCardsGenerator\helpers\Web\SpellsData.json", serializedSpellsData);
    }
  }

  private static SpellCombined Convert(Spell spell, SpellData spellData)
  {
    return new SpellCombined()
    {
      Name = spell.Name,
      Level = spell.Level,
      Link = spell.Link,
      School = spell.School,
      CastingTime = spell.CastingTime,
      Range = spell.Range,
      Duration = spell.Duration,
      Components = spell.Components,
      Source = spellData.Source,
      Description = spellData.Description,
      HigherLevelDesc = spellData.HigherLevelDesc,
      HasVerbal = spellData.HasVerbal,
      HasSemantic = spellData.HasSemantic,
      HasMaterial = spellData.HasMaterial,
      MaterialComponents = spellData.MaterialComponents,
      ContainsHtml = spellData.ContainsHtml,
    };
  }
}