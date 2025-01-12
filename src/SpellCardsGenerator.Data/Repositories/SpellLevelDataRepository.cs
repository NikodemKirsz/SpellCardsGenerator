using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class SpellLevelDataRepository : EntityRepository<int, SpellLevelData, SpellCardsDataContext>
{
  public SpellLevelDataRepository(
    ILogger<SpellLevelDataRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}