using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SchoolContent : GlobalizedEntity<int>
{
  [MaxUtf16Length(31)]
  public string Name { get; set; } = null!;

  public SchoolData Data { get; set; } = null!;
}