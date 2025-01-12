namespace SpellCardsGenerator.Common.Models.Documents;

public abstract record Document
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Name { get; set; } = null!;
}