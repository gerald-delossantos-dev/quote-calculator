using LoanCalculator.Application.Quote.Dtos;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Quote.Interfaces
{
    public interface IQuoteCommand
    {
        Task<QuoteViewModel> Create(QuotePostModel model);
        Task<bool> Update(QuotePostModel model);
        Task<bool> Delete(long id);
    }
}
