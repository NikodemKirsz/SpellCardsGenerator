using SpellCardsGenerator.Common;

namespace SpellCardsGenerator.Data.Models;

public sealed record School
{
  public int Id { get; set; }
  public string Language { get; set; } = Consts.DefaultLanguage;
  public string Slug { get; set; } = null!;
  public string Name { get; set; } = null!;
}