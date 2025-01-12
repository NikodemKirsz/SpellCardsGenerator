namespace SpellCardsGenerator.Common.Models.Documents;

public sealed record PdfDocument : Document
{
  public byte[] Data { get; set; } = Array.Empty<byte>();
}