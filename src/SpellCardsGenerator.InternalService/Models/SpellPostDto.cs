using SpellCardsGenerator.Common;

namespace SpellCardsGenerator.InternalService.Models;

public sealed record SpellPostDto
{
  public string Language { get; set; } = Consts.DefaultLanguage;
  public string Slug { get; set; } = null!;
  public string Name { get; set; } = null!;
  public int SpellLevelId { get; set; }
  public int SchoolId { get; set; }
  public bool IsRitual { get; set; }
  public string CastingTime { get; set; } = null!;
  public string Range { get; set; } = null!;
  public string Duration { get; set; } = null!;
  public bool HasVerbal { get; set; }
  public bool HasSemantic { get; set; }
  public bool HasMaterial { get; set; }
  public string? MaterialComponents { get; set; }
  public string DescriptionHtml { get; set; } = null!;
  public string? HigherLevelsDescription { get; set; }
}