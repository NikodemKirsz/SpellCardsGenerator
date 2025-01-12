using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class SchoolDataRepository : EntityRepository<int, SchoolData, SpellCardsDataContext>
{
  public SchoolDataRepository(
    ILogger<SchoolDataRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}