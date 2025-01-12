using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record SchoolData : Entity<int>
{
  [MaxUtf16Length(15)]
  public string Slug { get; set; } = null!;

  public virtual ICollection<SchoolContent> Contents { get; set; } = new List<SchoolContent>();
}