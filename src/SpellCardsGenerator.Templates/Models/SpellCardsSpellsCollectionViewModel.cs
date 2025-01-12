namespace SpellCardsGenerator.Templates.Models;

public sealed record SpellCardsSpellsCollectionViewModel
{
  public SpellCardsSpellViewModel[] Spells { get; set; } = Array.Empty<SpellCardsSpellViewModel>();
}