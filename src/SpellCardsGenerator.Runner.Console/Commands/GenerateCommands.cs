using Cocona;
using Cocona.Application;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Common.Models.Documents;
using SpellCardsGenerator.ConsoleRunner.Models;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.InternalService.Services;
using SpellCardsGenerator.InternalService.Services.Interfaces;

namespace SpellCardsGenerator.ConsoleRunner.Commands;

public sealed class GenerateCommands
{
  private readonly ILogger<GenerateCommands> _logger;
  private readonly IGenerate _generate;
  private readonly DataService _dataService;
  private readonly ICoconaAppContextAccessor _coconaAppContextAccessor;

  public GenerateCommands(
    ILogger<GenerateCommands> logger,
    IGenerate generate,
    DataService dataService,
    ICoconaAppContextAccessor coconaAppContextAccessor)
  {
    _logger = logger;
    _generate = generate;
    _dataService = dataService;
    _coconaAppContextAccessor = coconaAppContextAccessor;
  }

  [Command("generate")]
  public async Task Generate(
    [Option('t')] DocumentType type)
  {
    _logger.LogInformation("Generate method called");
    CoconaAppContext context = GetAppContext();
    CancellationToken cancellationToken = context.CancellationToken;
    
    await _generate.Initialize();

    SpellCardsData spells = await _dataService.GetSpellCardsData([1, 2, 3, 4], "pl", cancellationToken);

    switch (type)
    {
      case DocumentType.Pdf:
        PdfDocument pdfDocument = await _generate.Pdf(spells, cancellationToken);
        await File.WriteAllBytesAsync(@$"C:\Users\nikod\Temp\{pdfDocument.Name}", pdfDocument.Data, cancellationToken);
        break;
      case DocumentType.Html:
        HtmlDocument htmlDocument = await _generate.Html(spells, cancellationToken);
        await File.WriteAllTextAsync(@$"C:\Users\nikod\Temp\{htmlDocument.Name}", htmlDocument.Data, cancellationToken);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(type));
    }
  }

  private CoconaAppContext GetAppContext()
  {
    return _coconaAppContextAccessor.Current
      ?? throw new InvalidOperationException("Cocona App Context Current is null");
  }
}