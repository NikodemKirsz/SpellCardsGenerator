namespace SpellCardsGenerator.Data.Models;

public readonly struct GlobalizedId<TId> : IEquatable<GlobalizedId<TId>>, IComparable<GlobalizedId<TId>>
  where TId : IConvertible, IEquatable<TId>, IComparable<TId>
{
  public readonly TId Id;
  public readonly string LanguageId;

  public GlobalizedId(TId id, string languageId)
  {
    Id = id;
    LanguageId = languageId;
  }

  public int CompareTo(GlobalizedId<TId> other)
  {
    int idComparison = Id.CompareTo(other.Id);
    if (idComparison != 0) return idComparison;
    return String.Compare(LanguageId, other.LanguageId, StringComparison.Ordinal);
  }

  public static bool operator ==(GlobalizedId<TId> left, GlobalizedId<TId> right) => left.Equals(right);

  public static bool operator !=(GlobalizedId<TId> left, GlobalizedId<TId> right) => !left.Equals(right);

  public override bool Equals(object? obj)
  {
    return obj is GlobalizedId<TId> other && Equals(other);
  }

  public bool Equals(GlobalizedId<TId> other)
  {
    return EqualityComparer<TId>.Default.Equals(Id, other.Id) && LanguageId == other.LanguageId;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Id, LanguageId);
  }
}