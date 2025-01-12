using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Services.Abstract;

namespace SpellCardsGenerator.Data.Services;

public sealed class TemplateService : EntityService<string, TemplateContent, SpellCardsDataContext>
{
  public TemplateService(
    ILogger<TemplateService> logger,
    TemplateRepository templateRepository)
    : base(logger, templateRepository)
  { }

  private Template ConvertToModel(TemplateContent content)
  {
    return new Template()
    {
      LanguageId = content.Id,
      RitualLabel = content.RitualLabel,
      CastingTimeLabel = content.CastingTimeLabel,
      RangeLabel = content.RangeLabel,
      ComponentsLabel = content.ComponentsLabel,
      DurationLabel = content.DurationLabel,
      VerbalComponentSymbol = content.VerbalComponentSymbol,
      SemanticComponentSymbol = content.SemanticComponentSymbol,
      MaterialComponentSymbol = content.MaterialComponentSymbol,
      HigherLevelsLabel = content.HigherLevelsLabel,
    };
  }
}