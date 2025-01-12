namespace SpellCardsGenerator.Templates.Models;

public sealed record SpellCardsSpellViewModel
{
  public string Slug { get; set; } = null!;
  public string Name { get; set; } = null!;
  public int Level { get; set; }
  public string MagicSchool { get; set; } = null!;
  public string LevelName { get; set; } = null!;
  public bool IsRitual { get; set; }
  public string CastingTime { get; set; } = null!;
  public string Range { get; set; } = null!;
  public string Components { get; set; } = null!;
  public string Duration { get; set; } = null!;
  public string DescriptionHtml { get; set; } = null!;
  public string? HigherLevelsDescription { get; set; }
}