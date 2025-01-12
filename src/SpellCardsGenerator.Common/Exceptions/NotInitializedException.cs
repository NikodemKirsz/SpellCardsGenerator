namespace SpellCardsGenerator.Common.Exceptions;

[Serializable]
public class NotInitializedException : SpellCardsGeneratorException
{
  private const string Template = "Class '{0}' was not initialized!";
  
  public Type Type { get; }
  
  public NotInitializedException(Type type)
    : base(String.Format(Template, type.FullName))
  {
    Type = type;
  }
}