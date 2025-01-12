using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Repositories.Abstract;
using SpellCardsGenerator.Data.Services.Abstract;

namespace SpellCardsGenerator.Data.Services;

public sealed class LanguageService : EntityService<string, Language, SpellCardsDataContext>
{
  public LanguageService(
    ILogger<LanguageService> logger,
    LanguageRepository languageRepository)
    : base(logger, languageRepository)
  { }
}