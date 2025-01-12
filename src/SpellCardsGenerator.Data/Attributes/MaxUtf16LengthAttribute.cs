using System.ComponentModel.DataAnnotations;

namespace SpellCardsGenerator.Data.Attributes;

public class MaxUtf16LengthAttribute : MaxLengthAttribute
{
  private const int Utf16CharTOBytes = 8; 
  
  public MaxUtf16LengthAttribute(int length)
    : base(length * Utf16CharTOBytes)
  { }
}