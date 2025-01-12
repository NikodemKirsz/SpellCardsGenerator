namespace SpellCardsGenerator.Common.Models;

public readonly struct HtmlDimension : IEquatable<HtmlDimension>
{
  public static readonly string[] KnownUnits = ["px", "vh", "vw", "%", "cm", "mm", "in"];

  public readonly decimal Value;
  public readonly string Unit;

  public HtmlDimension(decimal value, string unit)
  {
    if (!KnownUnits.Contains(unit))
      throw new ArgumentException($"Unit '{unit}' is not recognized!", nameof(unit));

    Value = value;
    Unit = unit;
  }

  public static bool operator ==(HtmlDimension left, HtmlDimension right) => left.Equals(right);

  public static bool operator !=(HtmlDimension left, HtmlDimension right) => !(left == right);

  public override bool Equals(object? obj)
  {
    return obj is HtmlDimension other && Equals(other);
  }

  public bool Equals(HtmlDimension other)
  {
    return Value == other.Value && Unit == other.Unit;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Value, Unit);
  }

  public override string ToString()
  {
    return $"{Value}{Unit}";
  }
}