using LoanCalculator.Application.Calculation.Dtos;
using LoanCalculator.Application.Calculation.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Calculation.Commands
{
    public class PostQuoteCommand : IPostQuoteCommand
    {
        private const int httpTimeout = 10000; // todo: move to config
        private readonly ICalculationService _calculationService;

        public PostQuoteCommand(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        public async Task<QuoteSubmissionViewModel> Create(QuoteSubmissionPostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            var newCustomer = await _calculationService.CreateQuoteSubmissionAsync(model, cancellationTokenSource.Token);

            return newCustomer;
        }
    }
}
