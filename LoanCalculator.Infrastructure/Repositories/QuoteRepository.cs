using LoanCalculator.Core.Entities;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class QuoteRepository : BaseRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(ILoanCalculatorDBContext dbContext) : base(dbContext)
        { }
    }
}
