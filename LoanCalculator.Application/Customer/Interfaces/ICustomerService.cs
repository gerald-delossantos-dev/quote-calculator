using LoanCalculator.Application.Customer.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> GetCustomerByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<CustomerViewModel> AuthentiateUserAsync(string username, string password, CancellationToken cancellationToken = default);

        Task<IEnumerable<CustomerViewModel>> GetCustomersAsync(CancellationToken cancellationToken = default);

        Task<CustomerViewModel> CreateCustomersAsync(CustomerPostModel createModel, CancellationToken cancellationToken = default);

        Task<bool> UpdateCustomersAsync(CustomerPostModel updateModel, CancellationToken cancellationToken = default);

        Task<bool> DeleteCustomersAsync(long id, CancellationToken cancellationToken = default);
    }
}
