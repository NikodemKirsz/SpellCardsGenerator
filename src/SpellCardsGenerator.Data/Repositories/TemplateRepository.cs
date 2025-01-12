using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Repositories;

public sealed class TemplateRepository : EntityRepository<string, TemplateContent, SpellCardsDataContext>
{
  public TemplateRepository(
    ILogger<TemplateRepository> logger,
    SpellCardsDataContext context)
    : base(logger, context)
  { }
}