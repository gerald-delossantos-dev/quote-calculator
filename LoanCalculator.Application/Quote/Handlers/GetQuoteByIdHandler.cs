using LoanCalculator.Application.Quote.Dtos;
using LoanCalculator.Application.Quote.Interfaces;
using LoanCalculator.Application.Quote.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Quote.Handlers
{
    public class GetQuoteByIdHandler : IRequestHandler<GetQuotesByIdQuery, QuoteViewModel>
    {
        private readonly IQuoteService _quoteService;

        public GetQuoteByIdHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<QuoteViewModel> Handle(GetQuotesByIdQuery request,
                                                 CancellationToken cancellationToken)
        {
            return await _quoteService.GetQuoteByIdAsync(request.Id, cancellationToken);
        }
    }
}
