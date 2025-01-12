using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class SpellContentRepository : GlobalizedEntityRepository<int, SpellContent, SpellCardsDataContext>
{
  public SpellContentRepository(
    ILogger<SpellContentRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}