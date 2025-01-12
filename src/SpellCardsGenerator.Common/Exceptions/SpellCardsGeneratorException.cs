namespace SpellCardsGenerator.Common.Exceptions;

[Serializable]
public class SpellCardsGeneratorException : Exception
{
  private const string Template = $"{Consts.AppName} Exception! {{0}}";

  public SpellCardsGeneratorException()
    : this(null)
  {
  }

  public SpellCardsGeneratorException(string? message)
    : this(message, null)
  {
  }

  public SpellCardsGeneratorException(string? message, Exception? innerException)
    : base(String.Format(Template, message), innerException)
  {
  }
}