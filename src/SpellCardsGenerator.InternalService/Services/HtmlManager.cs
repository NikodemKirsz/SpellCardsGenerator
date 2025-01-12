using PuppeteerSharp;
using Razor.Templating.Core;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.InternalService.Services.Interfaces;
using SpellCardsGenerator.Templates.Models;

namespace SpellCardsGenerator.InternalService.Services;

internal class HtmlManager : IHtmlManager
{
  private static readonly Dictionary<string, object>? EmptyViewBag = new();
  private static readonly Lazy<string> MeasureMeasurementRectScript;
  private static readonly Lazy<string> MeasureSpellsRectScript;

  private readonly IRazorTemplateEngine _razorTemplateEngine;

  static HtmlManager()
  {
    var scriptsDir = Path.Combine(AppContext.BaseDirectory, "Scripts");

    MeasureMeasurementRectScript = new Lazy<string>(
      () => File.ReadAllText(Path.Combine(scriptsDir, "MeasureMeasurementRect.js"))
    );
    MeasureSpellsRectScript = new Lazy<string>(
      () => File.ReadAllText(Path.Combine(scriptsDir, "MeasureSpells.js"))
    );
  }

  public HtmlManager(IRazorTemplateEngine razorTemplateEngine)
  {
    _razorTemplateEngine = razorTemplateEngine;
  }

  public async Task<string> GenerateSpellCardsMeasurement(SpellCardsMeasurementViewModel model,
    CancellationToken token = default)
  {
    return await GenerateDocument(model, token);
  }

  public async Task<string> GenerateSpellCards(SpellCardsViewModel model, CancellationToken token = default)
  {
    return await GenerateDocument(model, token);
  }

  public async Task<(SpellMeasurementInfo[] spellInfos, int columnHeight)> GetMeasurementResults(IPage page)
  {
    Task<SpellMeasurementInfo[]> spellInfosTask = page.EvaluateExpressionAsync<SpellMeasurementInfo[]>(MeasureSpellsRectScript.Value);
    var measurementRectHeightTask = page.EvaluateExpressionAsync<int>(MeasureMeasurementRectScript.Value);

    await Task.WhenAll(spellInfosTask, measurementRectHeightTask);

    return (spellInfosTask.Result, measurementRectHeightTask.Result);
  }

  private async Task<string> GenerateDocument<TModel>(TModel model, CancellationToken token = default)
    where TModel : SpellCardsBaseViewModel
  {
    token.ThrowIfCancellationRequested();

    var html = await _razorTemplateEngine.RenderAsync("/Views/SpellCardsBaseView.cshtml", model, EmptyViewBag);
    return html;
  }
}