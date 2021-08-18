using LoanCalculator.Application.Quote.Dtos;
using MediatR;

namespace LoanCalculator.Application.Quote.Queries
{
    public class GetQuotesByIdQuery : IRequest<QuoteViewModel>
    {
        public long Id { get; set; }

        public GetQuotesByIdQuery(long id)
        {
            Id = id;
        }
    }
}
