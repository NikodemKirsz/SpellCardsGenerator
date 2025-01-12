using PuppeteerSharp;

namespace SpellCardsGenerator.InternalService.Services.Interfaces;

public interface IBrowserManager
{
  Task Initialize();
  Task<IPage> GetPage();
}