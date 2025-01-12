namespace SpellCardsGenerator.InternalService.Services.Organizers;

public sealed record ColumnOrganizerOptions
{
  //public int Columns { get; set; }
  public int ColumnHeight { get; set; }
  public int MaxSpellHeight { get; set; }
}