using System.Drawing;
using SpellCardsGenerator.Common.Models;

namespace SpellCardsGenerator.Common;

public static class Consts
{
  public const string AppName = nameof(SpellCardsGenerator);
  public const string DefaultLanguage = "en";
  public const int MinSpellLevel = 0;
  public const int MaxSpellLevel = 9;
  public static readonly HtmlDimension DefaultPageWidth = new(210, "mm");
  public static readonly HtmlDimension DefaultPageHeight = new(297, "mm");
  public static readonly HtmlDimension DefaultPageMargin = new(5, "mm");
  public static readonly HtmlDimension DefaultCardPadding = new(8, "px");
  public static readonly HtmlDimension DefaultFontSize = new(18, "px");
  public static readonly HtmlDimension DefaultSectionGap = new(16, "px");
  public const int DefaultColumnCount = 2;
  public static readonly HtmlDisplay DefaultLevelIndicatorDisplay = new(true);
  public static readonly Color DefaultLevelIndicatorColor0 = Color.FromArgb(255, 255, 0);
  public static readonly Color DefaultLevelIndicatorColor1 = Color.FromArgb(180, 199, 231);
  public static readonly Color DefaultLevelIndicatorColor2 = Color.FromArgb(47, 85, 151);
  public static readonly Color DefaultLevelIndicatorColor3 = Color.FromArgb(191, 149, 223);
  public static readonly Color DefaultLevelIndicatorColor4 = Color.FromArgb(112, 48, 160);
  public static readonly Color DefaultLevelIndicatorColor5 = Color.FromArgb(197, 224, 180);
  public static readonly Color DefaultLevelIndicatorColor6 = Color.FromArgb(84, 130, 53);
  public static readonly Color DefaultLevelIndicatorColor7 = Color.FromArgb(248, 203, 173);
  public static readonly Color DefaultLevelIndicatorColor8 = Color.FromArgb(197, 90, 17);
  public static readonly Color DefaultLevelIndicatorColor9 = Color.FromArgb(255, 0, 0);
}