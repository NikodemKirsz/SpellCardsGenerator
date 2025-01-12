using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class LanguageRepository : EntityRepository<string, Language, SpellCardsDataContext>
{
  public LanguageRepository(
    ILogger<LanguageRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}