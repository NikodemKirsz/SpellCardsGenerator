namespace SpellCardsGenerator.Templates.Models;

public sealed record SpellCardsMeasurementViewModel : SpellCardsBaseViewModel
{
  public SpellCardsSpellsCollectionViewModel Content { get; set; } = null!;
}