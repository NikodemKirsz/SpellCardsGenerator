using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Repositories.Abstract;
using SpellCardsGenerator.Data.Services.Abstract;

namespace SpellCardsGenerator.Data.Services;

public sealed class SpellLevelService : GlobalizedEntityService<int, SpellLevelData, SpellLevelContent, SpellLevel, SpellCardsDataContext>
{
  public SpellLevelService(
    ILogger<SpellLevelService> logger,
    SpellLevelDataRepository dataRepository,
    SpellLevelContentRepository contentRepository)
    : base(logger, dataRepository, contentRepository)
  {
  }

  protected override SpellLevel ConvertToModel(SpellLevelData data, SpellLevelContent content)
  {
    return new SpellLevel()
    {
      Id = data.Id,
      Language = content.LanguageId,
      Level = data.Level,
      Name = content.Name,
    };
  }

  protected override SpellLevelData ConvertToData(SpellLevel model)
  {
    return new SpellLevelData()
    {
      Id = model.Id,
      Level = model.Level,
    };
  }

  protected override SpellLevelContent ConvertToContent(SpellLevel model)
  {
    return new SpellLevelContent()
    {
      Id = model.Id,
      LanguageId = model.Language,
      Name = model.Name,
    };
  }
}