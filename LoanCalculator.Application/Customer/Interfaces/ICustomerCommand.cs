using LoanCalculator.Application.Customer.Dtos;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Interfaces
{
    public interface ICustomerCommand
    {
        Task<CustomerViewModel> Create(CustomerPostModel model);
        Task<bool> Update(CustomerPostModel model);
        Task<bool> Delete(long id);
    }
}
