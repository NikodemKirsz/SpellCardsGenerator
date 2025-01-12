using System.ComponentModel.DataAnnotations;
using SpellCardsGenerator.Common;
using SpellCardsGenerator.Data.Attributes;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Entities;

public record Language : Entity<string>
{
  [MaxUtf16Length(2)]
  public override string Id { get; set; } = null!;

  [MaxUtf16Length(31)]
  public string NameEn { get; set; } = null!;

  [MaxUtf16Length(31)]
  public string NameNative { get; set; } = null!;
  
  public virtual TemplateContent? Template { get; set; }
}