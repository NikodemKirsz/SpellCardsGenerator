using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SpellLevelContent : GlobalizedEntity<int>
{
  [MaxUtf16Length(31)]
  public string Name { get; set; } = null!;

  public SpellLevelData Data { get; set; } = null!;
}