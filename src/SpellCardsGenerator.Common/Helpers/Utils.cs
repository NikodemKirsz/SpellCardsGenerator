using System.Diagnostics;

namespace SpellCardsGenerator.Common.Helpers;

public static class Utils
{
  public static async Task Time(
    Func<int, Task> func,
    string name,
    int iterations,
    Action<string> logAction)
  {
    Stopwatch stopwatch = new();
    List<long> executionTimes = new(iterations);

    for (int i = 1; i <= iterations; i++)
    {
      stopwatch.Restart();

      await func(i);

      stopwatch.Stop();
      executionTimes.Add(stopwatch.ElapsedMilliseconds);
    }

    string executionTimesStr = String.Join(", ", executionTimes);
    double average = executionTimes.Average();
    logAction($"Method {name} took {average} on average, with consecutive: [{executionTimesStr}]");
  }
}