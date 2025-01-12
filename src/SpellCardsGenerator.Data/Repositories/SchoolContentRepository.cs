using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class SchoolContentRepository : GlobalizedEntityRepository<int, SchoolContent, SpellCardsDataContext>
{
  public SchoolContentRepository(
    ILogger<SchoolContentRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}