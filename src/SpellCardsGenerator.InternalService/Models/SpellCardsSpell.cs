using SpellCardsGenerator.Common.Models;

namespace SpellCardsGenerator.InternalService.Models;

public sealed record SpellCardsSpell
{
  public string Slug { get; set; } = null!;
  public string Name { get; set; } = null!;
  public Level Level { get; set; }
  public string MagicSchool { get; set; } = null!;
  public bool IsRitual { get; set; }
  public string CastingTime { get; set; } = null!;
  public string Range { get; set; } = null!;
  public SpellComponents Components { get; set; }
  public string Duration { get; set; } = null!;
  public string DescriptionHtml { get; set; } = null!;
  public string? HigherLevelDescription { get; set; }
}