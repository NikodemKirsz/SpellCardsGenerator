using System.Diagnostics;
using SpellCardsGenerator.InternalService.Services.Organizers;
using Xunit.Abstractions;

namespace SpellCardsGenerator.InternalService.Tests.Services.Organizers;

public class BruteforceColumnOrganizerTests
{
  private readonly BruteforceColumnOrganizer _sut = new();
  private readonly ITestOutputHelper _testOutputHelper;

  public BruteforceColumnOrganizerTests(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Theory]
  [InlineData(new[] { 1, 2, 3, 4 }, 10, 1)]
  [InlineData(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 9, 1)]
  [InlineData(new[] { 8, 5, 6, 2, 5}, 10, 3)]
  [InlineData(new[] { 8, 5, 6, 2, 4, 3, 1, 1, 1, 9}, 10, 4)]
  public void OrganizeTest(int[] numbers, int height, int result)
  {
    int[][] optimizedConfig = _sut.Organize(
      numbers,
      x => x,
      new ColumnOrganizerOptions() { ColumnHeight = height, MaxSpellHeight = height}
    );
    
    PrintPermutations(optimizedConfig);
    
    Assert.Equal(result, optimizedConfig.Length);
  }

  [Fact]
  public void SimpleGeneratePermutationsRecursiveTest()
  {
    List<int> array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    
    IEnumerable<int[]> permutations = BruteforceColumnOrganizer.SimpleGeneratePermutationsRecursive(
      [],
      array
    ).ToList();
    
    //PrintPermutations(permutations);
    
    Assert.Equal(3628800, permutations.Count());
  }

  /*[Fact]
  public void HeapsGeneratePermutationsRecursiveTest()
  {
    List<int> array = [1, 2, 3, 4];
    const int permutationsCount = 24;
    
    IEnumerable<int[]> permutations = BruteforceColumnOrganizer.HeapsGeneratePermutationsRecursive<int>(
      [],
      array,
      array.Count
    );
    
    //PrintPermutations(permutations);
    
    Assert.Equal(permutationsCount, permutations.Count());

    int uniqueCount = permutations
      .Select(permutation => String.Join("", permutation))
      .Distinct()
      .Count();
    Assert.Equal(permutationsCount, uniqueCount);
  }*/

  [Fact]
  public void HeapsGeneratePermutationsIterativeTest()
  {
    List<int> array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    const int permutationsCount = 3628800;
    
    IEnumerable<int[]> permutations = BruteforceColumnOrganizer.HeapsGeneratePermutationsIterative(
      array
    ).ToList();
    
    //PrintPermutations(permutations);
    
    Assert.Equal(permutationsCount, permutations.Count());

    int uniqueCount = permutations
      .Select(permutation => String.Join("", permutation))
      .Distinct()
      .Count();
    Assert.Equal(permutationsCount, uniqueCount);
  }

  [Fact]
  public void QuickPermCountdownGeneratePermutationsTest()
  {
    List<int> array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    const int permutationsCount = 3628800;
    
    IEnumerable<int[]> permutations = BruteforceColumnOrganizer.QuickPermCountdownGeneratePermutations(
      array
    ).ToList();
    
    //PrintPermutations(permutations);
    
    Assert.Equal(permutationsCount, permutations.Count());

    int uniqueCount = permutations
      .Select(permutation => String.Join("", permutation))
      .Distinct()
      .Count();
    Assert.Equal(permutationsCount, uniqueCount);
  }

  private void PrintPermutations<T>(IEnumerable<IEnumerable<T>> collections)
  {
    List<string> arrays = new(collections
      .Select(collection => String.Join(", ", collection))
      .Select(meat => $"[{meat}]")
    );

    string allPermutations = String.Join("\n", arrays.Select(arr => $"\t{arr}"));
    _testOutputHelper.WriteLine($"""
       Permutations:
       {allPermutations}
       ___
       """
    );
  }
}