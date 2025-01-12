namespace SpellCardsGenerator.Templates.Models;

public sealed record SpellCardsTemplateViewModel
{
  public string Lang { get; set; } = null!;
  public string PageWidth { get; set; } = null!;
  public string PageHeight { get; set; } = null!;
  public string PageMargin { get; set; } = null!;
  public string CardPadding { get; set; } = null!;
  public string FontSize { get; set; } = null!;
  public bool JustifyText { get; set; } //TODO
  public string SectionGap { get; set; } = null!;
  public int ColumnCount { get; set; }
  public string LevelIndicatorDisplay { get; set; } = null!;
  public string LevelIndicatorColor0 { get; set; } = null!;
  public string LevelIndicatorColor1 { get; set; } = null!;
  public string LevelIndicatorColor2 { get; set; } = null!;
  public string LevelIndicatorColor3 { get; set; } = null!;
  public string LevelIndicatorColor4 { get; set; } = null!;
  public string LevelIndicatorColor5 { get; set; } = null!;
  public string LevelIndicatorColor6 { get; set; } = null!;
  public string LevelIndicatorColor7 { get; set; } = null!;
  public string LevelIndicatorColor8 { get; set; } = null!;
  public string LevelIndicatorColor9 { get; set; } = null!;
  public string RitualLabel { get; set; } = null!;
  public string CastingTimeLabel { get; set; } = null!;
  public string RangeLabel { get; set; } = null!;
  public string ComponentsLabel { get; set; } = null!;
  public string DurationLabel { get; set; } = null!;
  public string HigherLevelsLabel { get; set; } = null!;
}