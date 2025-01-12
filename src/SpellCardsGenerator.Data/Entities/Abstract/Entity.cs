namespace SpellCardsGenerator.Data.Entities.Abstract;

public abstract record Entity<TId>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
  private static EqualityComparer Comparer { get; } = new();

  public virtual TId Id { get; set; } = default!;


  public virtual bool Equals(Entity<TId>? other) => Comparer.Equals(this, other);

  public override int GetHashCode() => Comparer.GetHashCode(this);

  
  private sealed class EqualityComparer : IEqualityComparer<Entity<TId>>
  {
    private static readonly EqualityComparer<TId> IdEqualityComparer = EqualityComparer<TId>.Default;

    public bool Equals(Entity<TId>? x, Entity<TId>? y)
    {
      if (ReferenceEquals(x, y))
        return true;
      if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
        return false;

      return x.GetType() == y.GetType() && IdEqualityComparer.Equals(x.Id, y.Id);
    }

    public int GetHashCode(Entity<TId> obj) => IdEqualityComparer.GetHashCode(obj.Id);
  }
}