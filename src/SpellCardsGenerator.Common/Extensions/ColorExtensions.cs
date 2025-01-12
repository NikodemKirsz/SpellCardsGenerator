using System.Drawing;

namespace SpellCardsGenerator.Common.Extensions;

public static class ColorExtensions
{
  public static string ToRgbaStr(this Color color)
  {
    var argbColor = (uint)color.ToArgb();
    var rgb = argbColor << 8;
    var alpha = argbColor >> 24;
    var rgbaColor = rgb + alpha;
    return $"#{rgbaColor:x8}";
  }
}