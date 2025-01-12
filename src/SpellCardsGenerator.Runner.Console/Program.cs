using Cocona;
using Cocona.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.ConsoleRunner.Commands;
using SpellCardsGenerator.InternalService.Infrastructure;

CoconaAppBuilder builder = CoconaApp.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .AddEnvironmentVariables()
  .Build();
builder.Configuration.AddConfiguration(config);
builder.Logging.AddConsole();

IServiceCollection services = builder.Services;
services.AddInternalService();

CoconaApp app = builder.Build();

app.AddCommands<GenerateCommands>();
app.AddCommands<WriteCommands>();

await app.RunAsync();