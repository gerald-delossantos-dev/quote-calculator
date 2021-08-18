using LoanCalculator.Application.Quote.Dtos;
using LoanCalculator.Application.Quote.Interfaces;
using LoanCalculator.Application.Quote.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Quote.Handlers
{
    public class GetQuotesHandler : IRequestHandler<GetQuotesQuery, IEnumerable<QuoteViewModel>>
    {
        private readonly IQuoteService _quoteService;

        public GetQuotesHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<IEnumerable<QuoteViewModel>> Handle(GetQuotesQuery request,
                                                              CancellationToken cancellationToken)
        {
            return await _quoteService.GetQuotesAsync(cancellationToken);
        }
    }
}
