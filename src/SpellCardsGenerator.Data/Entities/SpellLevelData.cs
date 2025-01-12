using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Common;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SpellLevelData : Entity<int>
{
  [Range(Consts.MinSpellLevel, Consts.MaxSpellLevel)]
  public int Level { get; set; }

  public virtual ICollection<SpellLevelContent> Contents { get; set; } = new List<SpellLevelContent>();
}