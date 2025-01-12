using Microsoft.Extensions.DependencyInjection;
using SpellCardsGenerator.InternalService.Services;
using SpellCardsGenerator.InternalService.Services.Interfaces;
using SpellCardsGenerator.Data.Infrastructure;

namespace SpellCardsGenerator.InternalService.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInternalService(this IServiceCollection services)
  {
    services.AddSpellsRepository();
    services.AddRazorTemplating();
    services.AddServices();

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddSingleton<IBrowserManager, BrowserManager>();
    services.AddSingleton<IHtmlManager, HtmlManager>();
    services.AddSingleton<IGenerate, Generate>();
    services.AddScoped<DataService>();

    return services;
  }
}