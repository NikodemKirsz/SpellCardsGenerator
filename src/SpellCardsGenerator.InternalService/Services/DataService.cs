using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Common.Models;
using SpellCardsGenerator.InternalService.Mapper;
using SpellCardsGenerator.InternalService.Models;
using SpellCardsGenerator.Data.Entities;
using SpellCardsGenerator.Data.Models;
using SpellCardsGenerator.Data.Repositories;
using SpellCardsGenerator.Data.Services;

namespace SpellCardsGenerator.InternalService.Services;

public sealed class DataService
{
  private readonly ILogger<DataService> _logger;
  private readonly LanguageRepository _languageRepository;
  private readonly SchoolService _schoolService;
  private readonly SpellLevelService _spellLevelService;
  private readonly SpellService _spellService;
  private readonly TemplateRepository _templateRepository;

  public DataService(
    ILogger<DataService> logger,
    LanguageRepository languageRepository,
    SchoolService schoolService,
    SpellLevelService spellLevelService,
    SpellService spellService,
    TemplateRepository templateRepository)
  {
    _logger = logger;
    _languageRepository = languageRepository;
    _schoolService = schoolService;
    _spellLevelService = spellLevelService;
    _spellService = spellService;
    _templateRepository = templateRepository;
  }

  public async Task<SpellCardsData> GetSpellCardsData(int[] spellIds, string languageId, CancellationToken token = default)
  {
    Spell[] spells = await _spellService.GetAllWithIds(spellIds, languageId, token);

    int[] schoolIds = spells
      .Select(static spell => spell.SchoolId)
      .Distinct()
      .ToArray();
    int[] spellLevelIds = spells
      .Select(static spell => spell.SpellLevelId)
      .Distinct()
      .ToArray();

    Task<School[]> schoolsTask = _schoolService.GetAllWithIds(schoolIds, languageId, token);
    Task<SpellLevel[]> spellLevelsTask = _spellLevelService.GetAllWithIds(spellLevelIds, languageId, token);
    Task<Template> templateTask = _templateRepository.Get(languageId, token);

    await Task.WhenAll(templateTask, schoolsTask, spellLevelsTask);

    Dictionary<int, School> schools = schoolsTask.Result.ToDictionary(static x => x.Id);
    Dictionary<int, SpellLevel> spellLevels = spellLevelsTask.Result.ToDictionary(static x => x.Id);
    Template template = templateTask.Result;

    _logger.LogInformation("Successfully got SpellCardsData for {Count} spells, with language {Language}",
      spellIds.Length, languageId);
    return new SpellCardsData()
    {
      Template = ModelConverter.ToSpellCardsTemplate(template),
      Spells = spells
        .Select(spell => ModelConverter.ToSpellCardsSpell(spell, schools, spellLevels, template))
        .ToArray(),
    };
  }

  public async Task<int> AddSpells(SpellPostDto[] spellPostDtos, CancellationToken token = default)
  {
    await _languageRepository.Add(new Language()
    {
      Id = "de",
      NameEn = "German",
      NameNative = "Deutsch",
    }, token);
    return 1;
    
    Spell[] spells = spellPostDtos
      .Select(ModelConverter.ToSpell)
      .ToArray();
    
    int entriesWritten = await _spellService.AddRange(spells, token: token);
    int spellsWritten = entriesWritten / 2;

    _logger.LogInformation("Successfully added {Count} Spells", spellsWritten);
    
    return spellsWritten;
  }

  private static SpellCardsSpell[] GenerateSpells()
  {
    return
    [
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 1",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(0, "sztuczka"),
        Range = "9 metrów",
        Slug = "mage_hand1",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 2",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(7, "7. krąg"),
        Range = "8 metrów",
        Slug = "mage_hand2",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 3",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(6, "6. krąg"),
        Range = "4 metrów",
        Slug = "mage_hand3",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 4",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(5, "5. krąg"),
        Range = "6 metrów",
        Slug = "mage_hand4",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 5",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(4, "4. krąg"),
        Range = "9 metrów",
        Slug = "mage_hand5",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 6",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(1, "1. krąg"),
        Range = "9 metrów",
        Slug = "mage_hand6",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Dłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Magiczna dłoń 7",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(2, "2. krąg"),
        Range = "9 metrów",
        Slug = "mage_hand7",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Dłoń magika nie znika nawet po zmroku",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(3, "3. krąg"),
        Range = "9 metrów",
        Slug = "hand_mage",
        CastingTime = "1 akcja",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Parówczak TV na propsie",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "godzina",
        Level = new Level(8, "8. krąg"),
        Range = "26 metrów",
        Slug = "tv_parowa",
        CastingTime = "1 minuta",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. Możesz używać jej do manipulowania przedmiotem, otwierania niezamkniętych na klucz drzwi lub pojemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      },
      new SpellCardsSpell()
      {
        Name = "Jak Buga Kobu tak Kub Bubie",
        Components = new SpellComponents("W", "S", null, null),
        Duration = "1 minuta",
        Level = new Level(9, "9. krąg"),
        Range = "1000 metrów",
        Slug = "kuba_buba",
        CastingTime = "1 minuta",
        DescriptionHtml =
          "W wybranym miejscu w zasięgu czaru pojawia się widmowa dłoń unosząca się w powietrzu. Będzie istniała przez minutę, ale zniknie wcześniej, jeśli odwołasz ją w ramach swojej akcji, znajdzie się dalej niż 9 metrów od ciebie albo ponownie rzucisz to zaklęcie.<br>\nMożesz przeznaczyć swoją akcję na kontrolowanie dłoni. jemnika, chowania lub wyjmowania przedmiotu z otwartego pojemnika albo wylania zawartości fiolki. Za każdym razem, gdy używasz dłoni, możesz przemieścić ją na odległość do 9&nbsp;metrów.<br>\nDłoń nie może atakować, uruchamiać magicznych przedmiotów ani nieść więcej niż 5 kilogramów.",
        MagicSchool = "Przywoływanie"
      }
    ];
  }
}