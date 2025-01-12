using SpellCardsGenerator.Common;

namespace SpellCardsGenerator.Data.Models;

public sealed record Template
{
  public string LanguageId { get; set; } = Consts.DefaultLanguage;
  public string RitualLabel { get; set; } = null!;
  public string CastingTimeLabel { get; set; } = null!;
  public string RangeLabel { get; set; } = null!;
  public string ComponentsLabel { get; set; } = null!;
  public string DurationLabel { get; set; } = null!;
  public string VerbalComponentSymbol { get; set; } = null!;
  public string SemanticComponentSymbol { get; set; } = null!;
  public string MaterialComponentSymbol { get; set; } = null!;
  public string HigherLevelsLabel { get; set; } = null!;
}