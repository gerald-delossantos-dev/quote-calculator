using LoanCalculator.Application.Calculation.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Calculation.Interfaces
{
    public interface ICalculationService
    {
        Task<QuoteSubmissionViewModel> CreateQuoteSubmissionAsync(QuoteSubmissionPostModel createModel, CancellationToken cancellationToken = default);
    }
}
