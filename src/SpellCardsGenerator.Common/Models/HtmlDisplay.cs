namespace SpellCardsGenerator.Common.Models;

public readonly struct HtmlDisplay
{
  private const string DisplayNone = "None";
  private static readonly string[] KnownValues = ["block", "inline", "flex", "grid", "table"];

  public readonly bool Visible;
  public readonly string DisplayValue;

  public HtmlDisplay(bool visible)
    : this(visible, KnownValues[0])
  {
  }

  public HtmlDisplay(bool visible, string displayValue)
  {
    if (!KnownValues.Contains(displayValue))
      throw new ArgumentException($"Display value '{displayValue}' is not recognized!", nameof(displayValue));

    Visible = visible;
    DisplayValue = displayValue;
  }

  public override string ToString()
  {
    return Visible ? DisplayValue : DisplayNone;
  }
}