using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record TemplateContent : Entity<string>
{
  [MaxLength(15)]
  public string RitualLabel { get; set; } = null!;
  
  [MaxLength(15)]
  public string CastingTimeLabel { get; set; } = null!;
  
  [MaxLength(15)]
  public string RangeLabel { get; set; } = null!;
  
  [MaxLength(15)]
  public string ComponentsLabel { get; set; } = null!;
  
  [MaxLength(15)]
  public string DurationLabel { get; set; } = null!;
  
  [MaxLength(1)]
  public string VerbalComponentSymbol { get; set; } = null!;
  
  [MaxLength(1)]
  public string SemanticComponentSymbol { get; set; } = null!;
  
  [MaxLength(1)]
  public string MaterialComponentSymbol { get; set; } = null!;

  [MaxLength(31)]
  public string HigherLevelsLabel { get; set; } = null!;

  public virtual Language? Language { get; set; }
}