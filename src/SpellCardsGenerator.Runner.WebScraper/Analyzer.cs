using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using SpellCardsGenerator.Runner.WebScraper.Models;

namespace SpellCardsGenerator.Runner.WebScraper;

public static class Analyzer
{
  private static readonly JsonSerializerOptions SerializerOptions = new()
  {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };
  
  public static async Task Start()
  {
    string spellDatasStr = await File.ReadAllTextAsync(@"C:\Repos\SpellCardsGenerator\helpers\Web\SpellsData.json");
    SpellCombined[] spellDatas = JsonSerializer.Deserialize<SpellCombined[]>(spellDatasStr) ?? throw new Exception("kurcze");

    var spellEntities = spellDatas
      .Where(spellData => spellData.Source.StartsWith("Play"))
      .OrderBy(spellData => spellData.Name)
      .Select(spellData => new
      {
        Id = 0,
        Language = "en",
        Slug = ToSlug(spellData.Name),
        Name = spellData.Name,
        SpellLevelId = spellData.Level + 1,
        SchoolId = ToSchoolId(spellData.School),
        IsRitual = spellData.CastingTime.Contains('R'),
        CastingTime = spellData.CastingTime.TrimEnd(' ', 'R'),
        Range = spellData.Range,
        Duration = spellData.Duration,
        HasVerbal = spellData.HasVerbal,
        HasSemantic = spellData.HasSemantic,
        HasMaterial = spellData.HasMaterial,
        MaterialComponents = spellData.MaterialComponents,
        DescriptionHtml = spellData.Description,
        HigherLevelsDescription = spellData.HigherLevelDesc,
      })
      .ToArray();
    
    string spellEntitiesStr = JsonSerializer.Serialize(spellEntities, SerializerOptions);
    await File.WriteAllTextAsync(@"C:\Repos\SpellCardsGenerator\helpers\Web\SpellEntities.json", spellEntitiesStr);

    int distinctCount = spellEntities.Select(x => x.Slug).Distinct().Count();
    Console.WriteLine($"Duplicates: {spellEntities.Length - distinctCount}");
    return;

    static string ToSlug(string name)
    {
      StringBuilder sb = new(10);
      return String.Join('_', name
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(part =>
        {
          sb.Clear();
          foreach (char lowerCharacter in part.Where(Char.IsLetter).Select(Char.ToLower))
          {
            sb.Append(lowerCharacter);
          }
          return sb.ToString();
        })
      );
    }

    static int ToSchoolId(string school)
    {
      string schoolCleaned = school.TrimEnd('D', 'C', 'T', ' ', 'G').ToLower();
      return schoolCleaned switch
      {
        "abjuration" => 1,
        "conjuration" => 2,
        "divination" => 3,
        "enchantment" => 4,
        "evocation" => 5,
        "illusion" => 6,
        "necromancy" => 7,
        "transmutation" => 8,
        _ => throw new ArgumentOutOfRangeException(nameof(school), school, null)
      };
    }
  }

  private static void Scraps(SpellCombined[] spellDatas )
  {
    Dictionary<string, int> sourcesToCount = spellDatas
      .GroupBy(x => x.Source)
      .OrderBy(x => x.Key)
      .ToDictionary(x => x.Key, y => y.Count());

    Dictionary<string, int> schoolsToCount = spellDatas
      .Where(x => x.Source.StartsWith("Play"))
      .GroupBy(x => x.School.TrimEnd('D', 'C', 'T', ' ', 'G'))
      .OrderBy(x => x.Key)
      .ToDictionary(x => x.Key, y => y.Count());
  } 
}