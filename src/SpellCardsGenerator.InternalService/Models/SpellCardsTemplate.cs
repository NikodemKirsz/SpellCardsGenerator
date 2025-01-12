using System.Drawing;
using SpellCardsGenerator.Common;
using SpellCardsGenerator.Common.Models;

namespace SpellCardsGenerator.InternalService.Models;

public sealed record SpellCardsTemplate
{
  public string Lang { get; set; } = Consts.DefaultLanguage;
  public HtmlDimension PageWidth { get; set; } = Consts.DefaultPageWidth;
  public HtmlDimension PageHeight { get; set; } = Consts.DefaultPageHeight;
  public HtmlDimension PageMargin { get; set; } = Consts.DefaultPageMargin;
  public HtmlDimension CardPadding { get; set; } = Consts.DefaultCardPadding;
  public HtmlDimension FontSize { get; set; } = Consts.DefaultFontSize;
  public HtmlDimension SectionGap { get; set; } = Consts.DefaultSectionGap;
  public int ColumnCount { get; set; } = Consts.DefaultColumnCount;
  public HtmlDisplay LevelIndicatorDisplay { get; set; } = Consts.DefaultLevelIndicatorDisplay;
  public Color LevelIndicatorColor0 { get; set; } = Consts.DefaultLevelIndicatorColor0;
  public Color LevelIndicatorColor1 { get; set; } = Consts.DefaultLevelIndicatorColor1;
  public Color LevelIndicatorColor2 { get; set; } = Consts.DefaultLevelIndicatorColor2;
  public Color LevelIndicatorColor3 { get; set; } = Consts.DefaultLevelIndicatorColor3;
  public Color LevelIndicatorColor4 { get; set; } = Consts.DefaultLevelIndicatorColor4;
  public Color LevelIndicatorColor5 { get; set; } = Consts.DefaultLevelIndicatorColor5;
  public Color LevelIndicatorColor6 { get; set; } = Consts.DefaultLevelIndicatorColor6;
  public Color LevelIndicatorColor7 { get; set; } = Consts.DefaultLevelIndicatorColor7;
  public Color LevelIndicatorColor8 { get; set; } = Consts.DefaultLevelIndicatorColor8;
  public Color LevelIndicatorColor9 { get; set; } = Consts.DefaultLevelIndicatorColor9;
  public string RitualLabel { get; set; } = null!;
  public string CastingTimeLabel { get; set; } = null!;
  public string RangeLabel { get; set; } = null!;
  public string ComponentsLabel { get; set; } = null!;
  public string DurationLabel { get; set; } = null!;
  public string HigherLevelsLabel { get; set; } = null!;
}