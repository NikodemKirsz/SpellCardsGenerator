namespace SpellCardsGenerator.InternalService.Models;

public sealed record SpellMeasurementInfo
{
  public string Slug { get; set; } = null!;
  public int Height { get; set; }
}