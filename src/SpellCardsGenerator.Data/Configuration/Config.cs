namespace SpellCardsGenerator.Data.Configuration;

public static class Config
{
  public sealed class Main
  {
    public required Connection Eredan { get; set; } = new();
  }

  public sealed class Connection
  {
    public string Host { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}