using BenchmarkDotNet.Attributes;
using SpellCardsGenerator.InternalService.Services.Organizers;

namespace SpellCardsGenerator.Benchmarks.Benchmarks;

/*
  With int[][]
  | Method                                 | Length | Mean           | Error         | StdDev        | Allocated    |
  |--------------------------------------- |------- |---------------:|--------------:|--------------:|-------------:|
  | SimpleGeneratePermutationsRecursive    | 5      |      16.327 us |     0.2901 us |     0.2713 us |     85.16 KB |
  | HeapsGeneratePermutationsRecursive     | 5      |       1.425 us |     0.0129 us |     0.0121 us |       8.8 KB |
  | HeapsGeneratePermutationsIterative     | 5      |       1.755 us |     0.0208 us |     0.0184 us |         8 KB |
  | QuickPermCountdownGeneratePermutations | 5      |       1.637 us |     0.0142 us |     0.0126 us |      8.09 KB |
  | SimpleGeneratePermutationsRecursive    | 9      | 139,953.104 us | 2,770.8249 us | 4,552.5448 us | 278553.46 KB |
  | HeapsGeneratePermutationsRecursive     | 9      |  24,844.063 us |   494.2665 us | 1,074.4946 us |  33708.15 KB |
  | HeapsGeneratePermutationsIterative     | 9      |  21,957.150 us |   437.4039 us |   568.7487 us |  29612.49 KB |
  | QuickPermCountdownGeneratePermutations | 9      |  21,660.510 us |   427.8559 us |   965.7420 us |  29612.52 KB |
  
  With IEnumerable<int[]>
  | Method                                 | Length | Mean          | Error         | StdDev        | Allocated    |
  |--------------------------------------- |------- |--------------:|--------------:|--------------:|-------------:|
  | SimpleGeneratePermutationsRecursive    | 5      |     16.374 us |     0.3186 us |     0.3129 us |     82.98 KB |
  | HeapsGeneratePermutationsRecursive     | 5      |      1.755 us |     0.0265 us |     0.0248 us |      7.88 KB |
  | HeapsGeneratePermutationsIterative     | 5      |      1.397 us |     0.0105 us |     0.0088 us |      5.81 KB |
  | QuickPermCountdownGeneratePermutations | 5      |      1.281 us |     0.0122 us |     0.0108 us |       5.9 KB |
  | SimpleGeneratePermutationsRecursive    | 9      | 58,958.914 us | 1,134.6194 us | 1,061.3237 us | 271621.46 KB |
  | HeapsGeneratePermutationsRecursive     | 9      | 21,068.561 us |   626.1442 us | 1,846.2003 us |  30873.36 KB |
  | HeapsGeneratePermutationsIterative     | 9      |  4,315.155 us |    58.8376 us |    55.0367 us |  22680.22 KB |
  | QuickPermCountdownGeneratePermutations | 9      |  3,829.649 us |    18.4539 us |    16.3589 us |  22680.31 KB |
  
  RESULT: Iterative is better (obviously), QuickPerm is a bit better than Heap's.
*/

[MemoryDiagnoser(false)]
public class PermutationsAlgorithms
{
  private static readonly Random _random = new(69);

  public volatile int[]? Result;
  private int[] Elements = null!;
  
  [Params(5, 9)]
  public int Length;

  [GlobalSetup]
  public void Setup()
  {
    Elements = Enumerable.Range(0, Length)
      .Select(static _ => _random.Next(0, 100))
      .ToArray();
  }
  
  [Benchmark]
  public void SimpleGeneratePermutationsRecursive()
  {
    IEnumerable<int[]> results = BruteforceColumnOrganizer.SimpleGeneratePermutationsRecursive([], Elements.ToList());
    foreach (int[] result in results)
    {
      Result = result;
    }
  }
  
  /*[Benchmark]
  public void HeapsGeneratePermutationsRecursive()
  {
    IEnumerable<int[]> results = BruteforceColumnOrganizer.HeapsGeneratePermutationsRecursive<int>([], Elements.ToList(), Elements.Length);
    foreach (int[] result in results)
    {
      Result = result;
    }
  }*/
  
  [Benchmark]
  public void HeapsGeneratePermutationsIterative()
  {
    IEnumerable<int[]> results = BruteforceColumnOrganizer.HeapsGeneratePermutationsIterative(Elements.ToList());
    foreach (int[] result in results)
    {
      Result = result;
    }
  }
  
  [Benchmark]
  public void QuickPermCountdownGeneratePermutations()
  {
    IEnumerable<int[]> results = BruteforceColumnOrganizer.QuickPermCountdownGeneratePermutations(Elements.ToList());
    foreach (int[] result in results)
    {
      Result = result;
    }
  }
}