using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class SpellDataRepository : EntityRepository<int, SpellData, SpellCardsDataContext>
{
  public SpellDataRepository(
    ILogger<SpellDataRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}