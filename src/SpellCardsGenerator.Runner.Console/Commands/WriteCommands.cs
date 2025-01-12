using System.Runtime.Serialization;
using System.Text.Json;
using Cocona;
using Cocona.Application;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.ConsoleRunner.Models;
using SpellCardsGenerator.ConsoleRunner.Validators;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.InternalService.Services;

namespace SpellCardsGenerator.ConsoleRunner.Commands;

public sealed class WriteCommands
{
  private readonly ILogger<WriteCommands> _logger;
  private readonly DataService _dataService;
  private readonly ICoconaAppContextAccessor _coconaAppContextAccessor;

  public WriteCommands(
    ILogger<WriteCommands> logger,
    DataService dataService,
    ICoconaAppContextAccessor coconaAppContextAccessor)
  {
    _logger = logger;
    _dataService = dataService;
    _coconaAppContextAccessor = coconaAppContextAccessor;
  }

  [Command("addspells")]
  public async Task AddSpells(
    [Option('f'), FilePathValidator] string filePath,
    [Option('t')] SourceFileType fileType = SourceFileType.JSON)
  {
    _logger.LogInformation("AddSpells method called");
    CoconaAppContext context = GetAppContext();
    CancellationToken cancellationToken = context.CancellationToken;

    string fileContent = await File.ReadAllTextAsync(filePath, cancellationToken);
    SpellPostDto[] spellPostDtos = JsonSerializer.Deserialize<SpellPostDto[]>(fileContent)
      ?? throw new SerializationException("Failed deserializing SpellPostDtos!");

    int addedCount = await _dataService.AddSpells(spellPostDtos, cancellationToken);
    _logger.LogInformation("Added {Count} spells from {Type} file: {Path}",
      addedCount, fileType, filePath);
  }

  private CoconaAppContext GetAppContext()
  {
    return _coconaAppContextAccessor.Current
      ?? throw new InvalidOperationException("Cocona App Context Current is null");
  }
}