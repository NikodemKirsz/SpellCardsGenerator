using SpellCardsGenerator.Common.Extensions;

namespace SpellCardsGenerator.Common.Models;

public readonly struct Level : IEquatable<Level>
{
  private const int MinValue = Consts.MinSpellLevel;
  private const int MaxValue = Consts.MaxSpellLevel;

  public readonly int Value;
  public readonly string Name;

  public Level(int value, string name)
  {
    if (!value.IsBetween(MinValue, MaxValue))
      throw new ArgumentException(
        $"{nameof(Level)} {nameof(Value)} must be in range {{{MinValue},{MaxValue}}}!",
        nameof(value)
      );

    Value = value;
    Name = name;
  }

  public static bool operator ==(Level left, Level right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(Level left, Level right)
  {
    return !(left == right);
  }

  public override bool Equals(object? obj)
  {
    return obj is Level other && Equals(other);
  }

  public bool Equals(Level other)
  {
    return Value == other.Value && Name == other.Name;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Value, Name);
  }
}