using SpellCardsGenerator.Common.Extensions;
using SpellCardsGenerator.Common.Models;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Templates.Models;

namespace SpellCardsGenerator.InternalService.Mapper;

internal static class ModelConverter
{
  public static Spell ToSpell(SpellPostDto spellPostDto)
  {
    return new Spell()
    {
      Id = default,
      Language = spellPostDto.Language,
      Slug = spellPostDto.Slug,
      Name = spellPostDto.Name,
      SpellLevelId = spellPostDto.SpellLevelId,
      SchoolId = spellPostDto.SchoolId,
      IsRitual = spellPostDto.IsRitual,
      CastingTime = spellPostDto.CastingTime,
      Range = spellPostDto.Range,
      Duration = spellPostDto.Duration,
      HasVerbal = spellPostDto.HasVerbal,
      HasSemantic = spellPostDto.HasSemantic,
      HasMaterial = spellPostDto.HasMaterial,
      MaterialComponents = spellPostDto.MaterialComponents,
      DescriptionHtml = spellPostDto.DescriptionHtml,
      HigherLevelsDescription = spellPostDto.HigherLevelsDescription,
    };
  }
  
  public static SpellCardsTemplateViewModel ToTemplateViewModel(SpellCardsTemplate spellCardsTemplate)
  {
    return new SpellCardsTemplateViewModel()
    {
      Lang = spellCardsTemplate.Lang,
      PageWidth = spellCardsTemplate.PageWidth.ToString(),
      PageHeight = spellCardsTemplate.PageHeight.ToString(),
      PageMargin = spellCardsTemplate.PageMargin.ToString(),
      CardPadding = spellCardsTemplate.CardPadding.ToString(),
      FontSize = spellCardsTemplate.FontSize.ToString(),
      SectionGap = spellCardsTemplate.SectionGap.ToString(),
      ColumnCount = spellCardsTemplate.ColumnCount,
      LevelIndicatorDisplay = spellCardsTemplate.LevelIndicatorDisplay.ToString(),
      LevelIndicatorColor0 = spellCardsTemplate.LevelIndicatorColor0.ToRgbaStr(),
      LevelIndicatorColor1 = spellCardsTemplate.LevelIndicatorColor1.ToRgbaStr(),
      LevelIndicatorColor2 = spellCardsTemplate.LevelIndicatorColor2.ToRgbaStr(),
      LevelIndicatorColor3 = spellCardsTemplate.LevelIndicatorColor3.ToRgbaStr(),
      LevelIndicatorColor4 = spellCardsTemplate.LevelIndicatorColor4.ToRgbaStr(),
      LevelIndicatorColor5 = spellCardsTemplate.LevelIndicatorColor5.ToRgbaStr(),
      LevelIndicatorColor6 = spellCardsTemplate.LevelIndicatorColor6.ToRgbaStr(),
      LevelIndicatorColor7 = spellCardsTemplate.LevelIndicatorColor7.ToRgbaStr(),
      LevelIndicatorColor8 = spellCardsTemplate.LevelIndicatorColor8.ToRgbaStr(),
      LevelIndicatorColor9 = spellCardsTemplate.LevelIndicatorColor9.ToRgbaStr(),
      RitualLabel = spellCardsTemplate.RitualLabel,
      CastingTimeLabel = spellCardsTemplate.CastingTimeLabel,
      RangeLabel = spellCardsTemplate.RangeLabel,
      ComponentsLabel = spellCardsTemplate.ComponentsLabel,
      DurationLabel = spellCardsTemplate.DurationLabel,
      HigherLevelsLabel = spellCardsTemplate.HigherLevelsLabel,
    };
  }

  public static SpellCardsSpellViewModel ToSpellCardsSpellViewModel(SpellCardsSpell spellCardsSpell)
  {
    return new SpellCardsSpellViewModel()
    {
      Slug = spellCardsSpell.Slug,
      Name = spellCardsSpell.Name,
      Level = spellCardsSpell.Level.Value,
      MagicSchool = spellCardsSpell.MagicSchool,
      LevelName = spellCardsSpell.Level.Name,
      IsRitual = spellCardsSpell.IsRitual,
      CastingTime = spellCardsSpell.CastingTime,
      Range = spellCardsSpell.Range,
      Components = spellCardsSpell.Components.ToString(),
      Duration = spellCardsSpell.Duration,
      DescriptionHtml = spellCardsSpell.DescriptionHtml,
      HigherLevelsDescription = spellCardsSpell.HigherLevelDescription,
    };
  }

  public static SpellCardsTemplate ToSpellCardsTemplate(Template template)
  {
    return new SpellCardsTemplate()
    {
      Lang = template.LanguageId,
      RitualLabel = template.RitualLabel,
      CastingTimeLabel = template.CastingTimeLabel,
      RangeLabel = template.RangeLabel,
      ComponentsLabel = template.ComponentsLabel,
      DurationLabel = template.DurationLabel,
      HigherLevelsLabel = template.HigherLevelsLabel,
    };
  }

  public static SpellCardsSpell ToSpellCardsSpell(
    Spell spell,
    Dictionary<int, School> schools,
    Dictionary<int, SpellLevel> spellLevels,
    Template template)
  {
    School school = schools[spell.SchoolId];
    SpellLevel spellLevel = spellLevels[spell.SpellLevelId];
          
    return new SpellCardsSpell()
    {
      Name = spell.Name,
      Slug = spell.Slug,
      Level = new Level(spellLevel.Level, spellLevel.Name),
      MagicSchool = school.Name,
      IsRitual = spell.IsRitual,
      CastingTime = spell.CastingTime,
      Range = spell.Range,
      Duration = spell.Duration,
      DescriptionHtml = spell.DescriptionHtml,
      HigherLevelDescription = spell.HigherLevelsDescription,
      Components = new SpellComponents(
        verbal: spell.HasVerbal ? template.VerbalComponentSymbol : null,
        semantic: spell.HasSemantic ? template.SemanticComponentSymbol : null,
        material: spell.HasMaterial ? template.MaterialComponentSymbol : null,
        materialComponents: spell.MaterialComponents
      ),
    };
  }
}