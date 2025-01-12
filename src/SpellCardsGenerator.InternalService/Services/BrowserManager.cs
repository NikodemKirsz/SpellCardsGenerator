using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using SpellCardsGenerator.Common.Exceptions;
using SpellCardsGenerator.InternalService.Services.Interfaces;

namespace SpellCardsGenerator.InternalService.Services;

internal class BrowserManager : IBrowserManager, IDisposable, IAsyncDisposable
{
  private const SupportedBrowser DefaultBrowser = SupportedBrowser.Chrome;
  private static readonly string PuppeteerCacheDir = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
    "PuppeteerCache"
  );
  private static readonly SemaphoreSlim InitializationLock = new(1, 1);

  private readonly ILogger<BrowserManager> _logger;
  private IBrowser? _browser = null;

  public BrowserManager(ILogger<BrowserManager> logger)
  {
    _logger = logger;
  }

  public async Task Initialize()
  {
    await InitializationLock.WaitAsync();
    if (_browser is not null)
    {
      InitializationLock.Release();
      return;
    }

    _logger.LogInformation("Initializing BrowserManager. Cache directory: '{CacheDir}'",
      PuppeteerCacheDir);

    BrowserFetcher browserFetcher = new(DefaultBrowser) { CacheDir = PuppeteerCacheDir };

    var installedBrowser = browserFetcher.GetInstalledBrowsers()
      .FirstOrDefault(browser => browser.Browser == DefaultBrowser);

    if (installedBrowser is null)
    {
      _logger.LogInformation("Cached browser not found, installing new browser");

      ProgressNotifier<BrowserManager> progressNotifier = new(_logger, 20);
      browserFetcher.DownloadProgressChanged += progressNotifier.ChangedHandler;
      installedBrowser = await browserFetcher.DownloadAsync();
      progressNotifier.LogFinal();
    }
    else
    {
      _logger.LogInformation("Cached browser found in '{ExecutablePath}'",
        installedBrowser.GetExecutablePath());
    }

    _browser = await Puppeteer.LaunchAsync(new LaunchOptions()
    {
      ExecutablePath = installedBrowser.GetExecutablePath(),
      Browser = DefaultBrowser,
      Headless = true
    });

    InitializationLock.Release();
  }

  public async Task<IPage> GetPage()
  {
    EnsureInitialization();

    return await _browser!.NewPageAsync();
  }

  private void EnsureInitialization()
  {
    if (_browser is null)
      throw new NotInitializedException(typeof(BrowserManager));
  }

  public void Dispose()
  {
    if (_browser is not null)
      _browser.Dispose();

    GC.SuppressFinalize(this);
  }

  public async ValueTask DisposeAsync()
  {
    if (_browser is not null)
      await _browser.DisposeAsync();

    GC.SuppressFinalize(this);
  }
}