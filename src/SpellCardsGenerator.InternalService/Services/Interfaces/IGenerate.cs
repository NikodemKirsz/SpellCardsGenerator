using SpellCardsGenerator.Common.Models.Documents;
using SpellCardsGenerator.InternalService.Models;

namespace SpellCardsGenerator.InternalService.Services.Interfaces;

public interface IGenerate
{
  Task<PdfDocument> Pdf(SpellCardsData spellCardsData, CancellationToken token = default);
  Task<HtmlDocument> Html(SpellCardsData spellCardsData, CancellationToken token = default);
  Task Initialize();
}