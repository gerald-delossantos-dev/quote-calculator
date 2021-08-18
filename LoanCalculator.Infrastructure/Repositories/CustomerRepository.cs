using LoanCalculator.Core.Entities;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ILoanCalculatorDBContext dbContext) : base(dbContext)
        { }
    }
}
