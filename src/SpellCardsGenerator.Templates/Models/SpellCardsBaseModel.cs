namespace SpellCardsGenerator.Templates.Models;

public abstract record SpellCardsBaseViewModel
{
  public SpellCardsTemplateViewModel Template { get; set; } = null!;
}