using System.Numerics;

namespace SpellCardsGenerator.Common.Extensions;

public static class NumberExtensions
{
  public static bool IsBetween<T>(this T value, T min, T max)
    where T : INumberBase<T>, IComparable<T>
  {
    if (value.CompareTo(min) < 0)
      return false;

    if (value.CompareTo(max) > 0)
      return false;

    return true;
  }
}