using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SpellContent : GlobalizedEntity<int>
{
  [MaxUtf16Length(63)]
  public string Name { get; set; } = null!;

  [MaxUtf16Length(4095)]
  public string DescriptionHtml { get; set; } = null!;

  [MaxUtf16Length(511)]
  public string? HigherLevelsDescription { get; set; }

  [MaxUtf16Length(31)]
  public string CastingTime { get; set; } = null!;

  [MaxUtf16Length(31)]
  public string Range { get; set; } = null!;

  [MaxUtf16Length(31)]
  public string Duration { get; set; } = null!;

  [MaxUtf16Length(511)]
  public string? MaterialComponents { get; set; }

  public SpellData Data { get; set; } = null!;
}