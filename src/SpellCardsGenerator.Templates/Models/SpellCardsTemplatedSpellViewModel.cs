namespace SpellCardsGenerator.Templates.Models;

public readonly struct SpellCardsTemplatedSpellViewModel
{
  public readonly SpellCardsSpellViewModel Spell;
  public readonly SpellCardsTemplateViewModel Template;

  public SpellCardsTemplatedSpellViewModel(SpellCardsSpellViewModel spell, SpellCardsTemplateViewModel template)
  {
    Spell = spell;
    Template = template;
  }
}