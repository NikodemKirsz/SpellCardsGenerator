namespace SpellCardsGenerator.InternalService.Services.Organizers.Interface;

public interface IColumnOrganizer
{
  T[][] Organize<T>(T[] elements,
    Func<T, int> heightExtractor,
    ColumnOrganizerOptions options);
}