namespace SpellCardsGenerator.Templates.Models;

public record SpellCardsViewModel : SpellCardsBaseViewModel
{
  public SpellCardsContentViewModel Content { get; set; } = null!;
}