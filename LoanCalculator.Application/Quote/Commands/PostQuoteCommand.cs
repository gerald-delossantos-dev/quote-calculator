using LoanCalculator.Application.Quote.Dtos;
using LoanCalculator.Application.Quote.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Quote.Commands
{
    public class PostQuoteCommand : IQuoteCommand
    {
        private const int httpTimeout = 10000; // todo: move to config
        private readonly IQuoteService _quoteService;

        public PostQuoteCommand(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<QuoteViewModel> Create(QuotePostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            var newCustomer = await _quoteService.CreateQuoteAsync(model, cancellationTokenSource.Token);

            return newCustomer;
        }

        public async Task<bool> Update(QuotePostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _quoteService.UpdateQuoteAsync(model, cancellationTokenSource.Token);
        }

        public async Task<bool> Delete(long id)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _quoteService.DeleteQuoteAsync(id, cancellationTokenSource.Token);
        }
    }
}