namespace SpellCardsGenerator.Templates.Models;

public sealed record SpellCardsContentViewModel
{
  public SpellCardsSpellsCollectionViewModel[] Pages { get; set; } = Array.Empty<SpellCardsSpellsCollectionViewModel>();
}