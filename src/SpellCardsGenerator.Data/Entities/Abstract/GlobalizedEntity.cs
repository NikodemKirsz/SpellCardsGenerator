using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;

namespace SpellCardsGenerator.Data.Entities.Abstract;

public abstract record GlobalizedEntity<TId> : Entity<TId>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
  private static EqualityComparer Comparer { get; } = new();

  [MaxUtf16Length(2)]
  public string LanguageId { get; set; } = null!;

  public virtual Language? Language { get; set; }


  public virtual bool Equals(GlobalizedEntity<TId>? other) => Equals(this, other) && Comparer.Equals(this, other);

  public override int GetHashCode() => Comparer.GetHashCode(this);

  
  private sealed class EqualityComparer : IEqualityComparer<GlobalizedEntity<TId>>
  {
    private static readonly EqualityComparer<string> LanguageEqualityComparer = EqualityComparer<string>.Default;

    public bool Equals(GlobalizedEntity<TId>? x, GlobalizedEntity<TId>? y)
    {
      if (ReferenceEquals(x, y))
        return true;
      if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
        return false;

      return x.GetType() == y.GetType() && LanguageEqualityComparer.Equals(x.LanguageId, y.LanguageId);
    }

    public int GetHashCode(GlobalizedEntity<TId> obj) => LanguageEqualityComparer.GetHashCode(obj.LanguageId);
  }
}