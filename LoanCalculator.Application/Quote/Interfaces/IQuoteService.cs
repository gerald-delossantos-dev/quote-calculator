using LoanCalculator.Application.Quote.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Quote.Interfaces
{
    public interface IQuoteService
    {
        Task<QuoteViewModel> GetQuoteByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<QuoteViewModel>> GetQuotesAsync(CancellationToken cancellationToken = default);
        Task<QuoteViewModel> CreateQuoteAsync(QuotePostModel createModel, CancellationToken cancellationToken = default);
        Task<bool> UpdateQuoteAsync(QuotePostModel updateModel, CancellationToken cancellationToken = default);
        Task<bool> DeleteQuoteAsync(long id, CancellationToken cancellationToken = default);
    }
}
