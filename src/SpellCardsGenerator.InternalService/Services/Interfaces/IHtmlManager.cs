using PuppeteerSharp;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.Templates.Models;

namespace SpellCardsGenerator.InternalService.Services.Interfaces;

public interface IHtmlManager
{
  Task<string> GenerateSpellCardsMeasurement(SpellCardsMeasurementViewModel model, CancellationToken token = default);
  Task<string> GenerateSpellCards(SpellCardsViewModel model, CancellationToken token = default);
  Task<(SpellMeasurementInfo[] spellInfos, int columnHeight)> GetMeasurementResults(IPage page);
}