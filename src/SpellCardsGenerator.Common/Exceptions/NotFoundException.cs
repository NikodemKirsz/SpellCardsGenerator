using System.Reflection;

namespace SpellCardsGenerator.Common.Exceptions;

[Serializable]
public class NotFoundException : SpellCardsGeneratorException
{
  private const string Template = "Entity of type {0} with keys ({1}) was not found!";

  public Type Type { get; }
  public object Keys { get; }

  public NotFoundException(Type type, object keys)
    : base(string.Format(Template, type.FullName, ObjectToString(keys)))
  {
    Type = type;
    Keys = keys;
  }

  private static string ObjectToString(object keys)
  {
    Dictionary<string, object?> dict = ConvertToDictionary(keys);
    string content = StringifyDictionary(dict);
    return content;
  }

  private static Dictionary<string, object?> ConvertToDictionary(object keys)
  {
    PropertyInfo[] props = keys.GetType().GetProperties();
    Dictionary<string, object?> dictionary = props.ToDictionary(
      o => o.Name,
      o => o.GetValue(keys)
    );
    return dictionary;
  }

  private static string StringifyDictionary(Dictionary<string, object?> dict)
  {
    IEnumerable<string> contents = dict
      .Select((key, value) => $"{key}={value}");
    return String.Join(", ", contents);
  }
}