using System.Runtime.CompilerServices;
using SpellCardsGenerator.InternalService.Services.Organizers.Interface;

namespace SpellCardsGenerator.InternalService.Services.Organizers;

public sealed class BruteforceColumnOrganizer : IColumnOrganizer
{
  public T[][] Organize<T>(
    T[] elements,
    Func<T, int> heightExtractor,
    ColumnOrganizerOptions options)
  {
    if (elements.Length > 10)
      throw new InvalidOperationException("Bo mi komputer spalisz mordo...");

    IEnumerable<T[]> permutations = CreatePermutations(elements);
    int leastColumns = Int32.MaxValue;
    T[][]? optimalOrganization = null;

    foreach (T[] permutation in permutations)
    {
      T[][] organization = OrganizePermutation(permutation, heightExtractor, options.ColumnHeight);

      if (organization.Length < leastColumns)
      {
        leastColumns = organization.Length;
        optimalOrganization = organization;
      }
    }

    return optimalOrganization ?? [];
  }

  private static T[][] OrganizePermutation<T>(T[] elements, Func<T, int> heightExtractor, int columnHeight)
  {
    List<T[]> columns = new(elements.Length / 3);

    int spaceUsed = 0;
    List<T> columnElements = new(4);
    
    foreach (T element in elements)
    {
      int elementHeight = heightExtractor(element);
      int newHeight = spaceUsed + elementHeight;
      if (newHeight > columnHeight)
      {
        columns.Add(columnElements.ToArray());
        columnElements.Clear();
        newHeight = elementHeight;
      }

      columnElements.Add(element);
      spaceUsed = newHeight;
    }
    
    columns.Add(columnElements.ToArray());

    return columns.ToArray();
  }

  private static IEnumerable<T[]> CreatePermutations<T>(T[] elements)
  {
    return SimpleGeneratePermutationsRecursive([], elements.ToList());
    //return QuickPermCountdownGeneratePermutations<T>(elements.ToList());
  }

  public static IEnumerable<T[]> SimpleGeneratePermutationsRecursive<T>(
    List<T> currentPermutation,
    List<T> elementsToPermute)
  {
    if (elementsToPermute.Count > 0)
    {
      foreach (T element in elementsToPermute)
      {
        List<T> nextPermutation = [..currentPermutation, element];
        List<T> remainingElements = [..elementsToPermute];
        remainingElements.Remove(element);

        foreach (T[] permutation in SimpleGeneratePermutationsRecursive(nextPermutation, remainingElements))
        {
          yield return permutation;
        }
      }
    }
    else
    {
      yield return currentPermutation.ToArray();
    }
  }

  /* BROKEN!!!
   public static List<T[]> HeapsGeneratePermutationsRecursive<T>(
    List<T[]> generatedPermutations,
    List<T> elementsToPermute,
    int lengthOfArray)
  {
    if (lengthOfArray == 1)
    {
      generatedPermutations.Add(elementsToPermute.ToArray());
    }
    else
    {
      for (int i = 0; i < lengthOfArray; i++)
      {
        Swap(elementsToPermute, IsEven(lengthOfArray) ? i : 0, lengthOfArray - 1);
        HeapsGeneratePermutationsRecursive(generatedPermutations, elementsToPermute, lengthOfArray - 1);
      }
    }

    return generatedPermutations;
  }*/

  public static IEnumerable<T[]> HeapsGeneratePermutationsIterative<T>(List<T> elementsToPermute)
  {
    int length = elementsToPermute.Count;
    yield return elementsToPermute.ToArray();
    
    int[] temp = new int[length];
    int i = 0;
    while (i < length)
    {
      if (temp[i] < i)
      {
        Swap(elementsToPermute, IsEven(i) ? 0 : temp[i], i);
        
        yield return elementsToPermute.ToArray();

        temp[i] += 1;
        i = 0;
      }
      else
      {
        temp[i] = 0;
        i++;
      }
    }
  }

  public static IEnumerable<T[]> QuickPermCountdownGeneratePermutations<T>(List<T> elementsToPermute)
  {
    int length = elementsToPermute.Count;
    yield return elementsToPermute.ToArray();

    int[] p = Enumerable.Range(0, length + 1)
      .Select(static i => i)
      .ToArray();
    int i = 1;
    while (i < length)
    {
      p[i]--;
      
      int j = IsEven(i) ? 0 : p[i];
      Swap(elementsToPermute, j, i);
      
      yield return elementsToPermute.ToArray();

      i = 1;
      while (p[i] == 0)
      {
        p[i] = i;
        i++;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsEven(int number) => (number & 1) == 0;
  
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void Swap<T>(IList<T> collection, int index1, int index2)
  {
    (collection[index1], collection[index2]) = (collection[index2], collection[index1]);
  }
}