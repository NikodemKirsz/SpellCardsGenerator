using System.Net;
using Microsoft.Extensions.Logging;

namespace SpellCardsGenerator.InternalService.Services;

public class ProgressNotifier<T>
{
  private readonly ILogger<T> _logger;
  private readonly int _thresholdsCount;

  private long _total = 0;
  private int _completed = 0;

  public ProgressNotifier(ILogger<T> logger, int thresholdsCount)
  {
    _logger = logger;
    _thresholdsCount = thresholdsCount;
  }

  public void ChangedHandler(object sender, DownloadProgressChangedEventArgs args)
  {
    (long received, _total) = (args.BytesReceived, args.TotalBytesToReceive);
    bool isExtreme = _completed == 0 || _completed == _thresholdsCount;
    bool isThresholdReached = received >= _total * (_completed / (double)_thresholdsCount);
    
    if (isExtreme || isThresholdReached)
    {
      LogProgress(received);
      _completed++;
    }
  }

  public void LogFinal()
  {
    LogProgress(_total);
  }

  private void LogProgress(long received)
  {
    double percent = (received * 100.0) / _total;
    _logger.LogInformation("Downloading progress: {Received}/{Total}B, {Percent:F1}%",
      received, _total, percent);
  }
}