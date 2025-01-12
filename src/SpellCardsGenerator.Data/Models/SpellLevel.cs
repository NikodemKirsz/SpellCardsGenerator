using SpellCardsGenerator.Common;

namespace SpellCardsGenerator.Data.Models;

public record SpellLevel
{
  public int Id { get; set; }
  public string Language { get; set; } = Consts.DefaultLanguage;
  public int Level { get; set; }
  public string Name { get; set; } = null!;
}