using LoanCalculator.Application.Calculation.Dtos;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Calculation.Interfaces
{
    public interface IPostQuoteCommand
    {
        Task<QuoteSubmissionViewModel> Create(QuoteSubmissionPostModel model);
    }
}
