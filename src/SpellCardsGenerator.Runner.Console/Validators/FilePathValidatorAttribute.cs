using System.ComponentModel.DataAnnotations;

namespace SpellCardsGenerator.ConsoleRunner.Validators;

public class FilePathValidatorAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value is string path && File.Exists(path))
      return ValidationResult.Success;
    
    return new ValidationResult($"File path '{value}' is not found.");
  }
}