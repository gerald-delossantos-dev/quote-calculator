using LoanCalculator.Core.Entities;

namespace LoanCalculator.Infrastructure.Interfaces
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    { }
}
