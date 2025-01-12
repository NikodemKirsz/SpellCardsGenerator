using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SpellData : Entity<int>
{
  [MaxUtf16Length(63)]
  public string Slug { get; set; } = null!;

  public int SpellLevelId { get; set; }

  public int SchoolId { get; set; }
  
  public bool IsRitual { get; set; }

  public bool HasVerbal { get; set; } = false;

  public bool HasSemantic { get; set; } = false;

  public bool HasMaterial { get; set; } = false;
  

  public virtual SpellLevelData? SpellLevel { get; set; }

  public virtual SchoolData? School { get; set; }
  
  public virtual ICollection<SpellContent> Contents { get; set; } = new List<SpellContent>();
}