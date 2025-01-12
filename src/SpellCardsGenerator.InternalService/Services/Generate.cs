using PuppeteerSharp;
using PuppeteerSharp.Media;
using SpellCardsGenerator.Common.Models.Documents;
using SpellCardsGenerator.InternalService.Mapper;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.InternalService.Services.Interfaces;
using SpellCardsGenerator.Templates.Models;

namespace SpellCardsGenerator.InternalService.Services;

internal class Generate : IGenerate
{
  private static readonly MarginOptions ZeroMargin = new() { Right = "0px", Bottom = "0px", Left = "0px", Top = "0px" };

  private readonly object _lock = new();
  private readonly IBrowserManager _browserManager;
  private readonly IHtmlManager _htmlManager;

  private bool _initialized = false;

  public Generate(IBrowserManager browserManager, IHtmlManager htmlManager)
  {
    _browserManager = browserManager;
    _htmlManager = htmlManager;
  }

  public async Task<PdfDocument> Pdf(SpellCardsData spellCardsData, CancellationToken token = default)
  {
    await Initialize();

    await using var page = await _browserManager.GetPage();

    await OrganizeSpells(page, spellCardsData, token);

    var pdfData = await page.PdfDataAsync(new PdfOptions()
    {
      MarginOptions = ZeroMargin,
      Format = PaperFormat.A4,
      PrintBackground = true,
      PreferCSSPageSize = true,
    });

    return new PdfDocument()
    {
      Name = "Spells.pdf",
      Data = pdfData,
    };
  }

  public async Task<HtmlDocument> Html(SpellCardsData spellCardsData, CancellationToken token = default)
  {
    await Initialize();

    await using var page = await _browserManager.GetPage();

    await OrganizeSpells(page, spellCardsData, token);

    var htmlData = await page.GetContentAsync();

    return new HtmlDocument()
    {
      Name = "Spells.html",
      Data = htmlData,
    };
  }

  public async Task Initialize()
  {
    if (_initialized)
      return;

    await _browserManager.Initialize();

    lock (_lock)
    {
      _initialized = true;
    }
  }

  private async Task OrganizeSpells(IPage page, SpellCardsData spellCardsData, CancellationToken token = default)
  {
    var templateViewModel = ModelConverter.ToTemplateViewModel(spellCardsData.Template);
    SpellCardsSpellViewModel[] spellCardsSpellViewModels = spellCardsData.Spells
      .Select(ModelConverter.ToSpellCardsSpellViewModel)
      .ToArray();

    SpellCardsMeasurementViewModel spellCardsMeasurementViewModel = new()
    {
      Template = templateViewModel,
      Content = new SpellCardsSpellsCollectionViewModel() { Spells = spellCardsSpellViewModels }
    };
    var measurementHtml = await _htmlManager.GenerateSpellCardsMeasurement(spellCardsMeasurementViewModel, token);
    await page.SetContentAsync(measurementHtml);

    (SpellMeasurementInfo[] spellInfos, var columnHeight) = await _htmlManager.GetMeasurementResults(page);
    string[][] sortedSpells = SortSpellsIntoPages(spellInfos, columnHeight);

    Dictionary<string, SpellCardsSpellViewModel> slugToSpellViewModel = spellCardsSpellViewModels
      .ToDictionary(x => x.Slug);

    var spellCardsViewModel = ConvertToSpellViewModel(templateViewModel, sortedSpells, slugToSpellViewModel);
    var spellCardsHtml = await _htmlManager.GenerateSpellCards(spellCardsViewModel, token);
    await page.SetContentAsync(spellCardsHtml);
  }

  private static string[][] SortSpellsIntoPages(SpellMeasurementInfo[] spellInfos, int columnHeight)
  {
    SpellMeasurementInfo[] sortedSpellInfos = spellInfos
      .OrderByDescending(x => x.Height)
      .ToArray();

    List<List<string>> spellPages = [];

    var spaceUsed = 0;
    List<string> currentPage = [];
    foreach (var spellInfo in sortedSpellInfos)
    {
      if (spaceUsed + spellInfo.Height > columnHeight)
      {
        spellPages.Add(currentPage);
        currentPage = [];
        spaceUsed = 0;
      }

      currentPage.Add(spellInfo.Slug);
      spaceUsed += spellInfo.Height;
    }

    spellPages.Add(currentPage);

    return spellPages
      .Select(spellSlugs => spellSlugs.ToArray())
      .ToArray();
  }

  private static SpellCardsViewModel ConvertToSpellViewModel(
    SpellCardsTemplateViewModel templateViewModel,
    string[][] spellPages,
    Dictionary<string, SpellCardsSpellViewModel> slugToSpellViewModel)
  {
    return new SpellCardsViewModel()
    {
      Template = templateViewModel,
      Content = new SpellCardsContentViewModel()
      {
        Pages = spellPages
          .Select(spellSlugs => spellSlugs
            .Select(spellSlug => slugToSpellViewModel[spellSlug])
            .ToArray())
          .Select(spellViewModels => new SpellCardsSpellsCollectionViewModel()
          {
            Spells = spellViewModels
          })
          .ToArray()
      },
    };
  }
}