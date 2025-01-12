namespace SpellCardsGenerator.Runner.WebScraper.Models;

public record SpellCombined
{
  public string Name { get; set; } = null!;
  public int Level { get; set; }
  public string Link { get; set; } = null!;
  public string School { get; set; } = null!;
  public string CastingTime { get; set; } = null!;
  public string Range { get; set; } = null!;
  public string Duration { get; set; } = null!;
  public string Components { get; set; } = null!;
  public string Source { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string HigherLevelDesc { get; set; } = null!;
  public bool HasVerbal { get; set; }
  public bool HasSemantic { get; set; }
  public bool HasMaterial { get; set; }
  public string MaterialComponents { get; set; } = null!;
  public bool ContainsHtml { get; set; }
}