using LoanCalculator.Application.Quote.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LoanCalculator.Application.Quote.Queries
{
    public class GetQuotesQuery : IRequest<IEnumerable<QuoteViewModel>>
    { }
}
