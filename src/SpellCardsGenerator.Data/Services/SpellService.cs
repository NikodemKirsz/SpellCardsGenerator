using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Services.Abstract;

namespace SpellCardsGenerator.Data.Services;

public sealed class SpellService : GlobalizedEntityService<int, SpellData, SpellContent, Spell, SpellCardsDataContext>
{
  public SpellService(
    ILogger<SpellService> logger,
    SpellDataRepository dataRepository,
    SpellContentRepository contentRepository)
    : base(logger, dataRepository, contentRepository)
  { }
  
  public async Task<Spell[]> GetAllWithSlugs(string[] slugs, string languageId, CancellationToken token = default)
  {
    _logger.LogInformation("Getting multiple aggregated spells with language '{Language}' by slugs",
      languageId);

    int[] ids = await _entityDatas
      .Where(data => slugs.Contains(data.Slug))
      .Select(static data => data.Id)
      .ToArrayAsync(cancellationToken: token);

    Spell[] spells = await GetAllWithIds(ids, languageId, token);
    return spells;
  }

  protected override Spell ConvertToModel(SpellData data, SpellContent content)
  {
    return new Spell()
    {
      Id = data.Id,
      Language = content.LanguageId,
      Slug = data.Slug,
      Name = content.Name,
      SpellLevelId = data.SpellLevelId,
      SchoolId = data.SchoolId,
      IsRitual = data.IsRitual,
      CastingTime = content.CastingTime,
      MaterialComponents = content.MaterialComponents,
      Range = content.Range,
      Duration = content.Duration,
      HasVerbal = data.HasVerbal,
      HasSemantic = data.HasSemantic,
      HasMaterial = data.HasMaterial,
      DescriptionHtml = content.DescriptionHtml,
      HigherLevelsDescription = content.HigherLevelsDescription,
    };
  }

  protected override SpellData ConvertToData(Spell model)
  {
    return new SpellData()
    {
      Id = model.Id,
      Slug = model.Slug,
      SpellLevelId = model.SpellLevelId,
      SchoolId = model.SchoolId,
      IsRitual = model.IsRitual,
      HasVerbal = model.HasVerbal,
      HasSemantic = model.HasSemantic,
      HasMaterial = model.HasMaterial,
    };
  }

  protected override SpellContent ConvertToContent(Spell model)
  {
    return new SpellContent()
    {
      Id = model.Id,
      LanguageId = model.Language,
      Name = model.Name,
      DescriptionHtml = model.DescriptionHtml,
      HigherLevelsDescription = model.HigherLevelsDescription,
      CastingTime = model.CastingTime,
      Range = model.Range,
      Duration = model.Duration,
      MaterialComponents = model.MaterialComponents,
    };
  }
}