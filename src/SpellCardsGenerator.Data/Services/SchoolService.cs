using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Entities.Abstract;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Repositories.Abstract;
using SpellCardsGenerator.Data.Services.Abstract;

namespace SpellCardsGenerator.Data.Services;

public sealed class SchoolService : GlobalizedEntityService<int, SchoolData, SchoolContent, School, SpellCardsDataContext>
{
  public SchoolService(
    ILogger<SchoolService> logger,
    SchoolDataRepository dataRepository,
    SchoolContentRepository contentRepository)
    : base(logger, dataRepository, contentRepository)
  {
  }

  protected override School ConvertToModel(SchoolData data, SchoolContent content)
  {
    return new School()
    {
      Id = data.Id,
      Slug = data.Slug,
      Name = content.Name,
    };
  }

  protected override SchoolData ConvertToData(School model)
  {
    return new SchoolData()
    {
      Id = model.Id,
      Slug = model.Slug,
    };
  }

  protected override SchoolContent ConvertToContent(School model)
  {
    return new SchoolContent()
    {
      Id = model.Id,
      LanguageId = model.Language,
      Name = model.Name,
    };
  }
}