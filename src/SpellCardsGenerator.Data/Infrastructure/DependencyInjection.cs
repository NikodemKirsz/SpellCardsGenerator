using Microsoft.Extensions.DependencyInjection;
using SpellCardsGenerator.Data.Configuration;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Services;

namespace SpellCardsGenerator.Data.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddSpellsRepository(this IServiceCollection services)
  {
    services.AddConfiguration();
    services.AddDbServices();
    services.AddServices();

    return services;
  }

  private static IServiceCollection AddConfiguration(this IServiceCollection services)
  {
    services.AddOptions<Config.Main>()
      .BindConfiguration("Connections");

    return services;
  }

  private static IServiceCollection AddDbServices(this IServiceCollection services)
  {
    services.AddDbContextFactory<SpellCardsDataContext>();

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<LanguageRepository>();
    services.AddScoped<SchoolService>();
    services.AddScoped<SpellService>();
    services.AddScoped<SpellLevelService>();
    services.AddScoped<TemplateRepository>();
    
    return services;
  }
}