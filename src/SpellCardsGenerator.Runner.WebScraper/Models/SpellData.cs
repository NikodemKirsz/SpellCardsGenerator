namespace SpellCardsGenerator.Runner.WebScraper.Models;

public record SpellData
{
  public string Source { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string HigherLevelDesc { get; set; } = null!;
  public bool HasVerbal { get; set; }
  public bool HasSemantic { get; set; }
  public bool HasMaterial { get; set; }
  public string MaterialComponents { get; set; } = null!;
  public bool ContainsHtml { get; set; }
}