namespace SpellCardsGenerator.Common.Models.Documents;

public sealed record HtmlDocument : Document
{
  public string Data { get; set; } = null!;
}