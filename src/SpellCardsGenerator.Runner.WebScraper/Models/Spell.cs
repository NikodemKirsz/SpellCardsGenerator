namespace SpellCardsGenerator.Runner.WebScraper.Models;

public record Spell
{
  public string Name { get; set; } = null!;
  public int Level { get; set; }
  public string Link { get; set; } = null!;
  public string School { get; set; } = null!;
  public string CastingTime { get; set; } = null!;
  public string Range { get; set; } = null!;
  public string Duration { get; set; } = null!;
  public string Components { get; set; } = null!;
}