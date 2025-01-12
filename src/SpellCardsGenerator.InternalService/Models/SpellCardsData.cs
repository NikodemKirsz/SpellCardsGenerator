namespace SpellCardsGenerator.InternalService.Models;

public sealed record SpellCardsData
{
  public SpellCardsTemplate Template { get; set; } = null!;
  public SpellCardsSpell[] Spells { get; set; } = Array.Empty<SpellCardsSpell>();
}