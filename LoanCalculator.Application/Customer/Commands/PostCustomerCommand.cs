using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Commands
{
    public class PostCustomerCommand : ICustomerCommand
    {
        private const int httpTimeout = 10000; // todo: move to config
        private readonly ICustomerService _customerService;

        public PostCustomerCommand(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerViewModel> Create(CustomerPostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            var newCustomer = await _customerService.CreateCustomersAsync(model, cancellationTokenSource.Token);

            return newCustomer;
        }

        public async Task<bool> Update(CustomerPostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _customerService.UpdateCustomersAsync(model, cancellationTokenSource.Token);
        }

        public async Task<bool> Delete(long id)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _customerService.DeleteCustomersAsync(id, cancellationTokenSource.Token);
        }
    }
}
