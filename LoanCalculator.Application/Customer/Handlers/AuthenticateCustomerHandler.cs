using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Application.Customer.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Handlers
{
    public class AuthenticateCustomerHandler : IRequestHandler<AuthenticateCustomerQuery, CustomerViewModel>
    {
        private readonly ICustomerService _customerService;

        public AuthenticateCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerViewModel> Handle(AuthenticateCustomerQuery request,
                                                    CancellationToken cancellationToken)
        {
            return await _customerService.AuthentiateUserAsync(request.UserName, request.Password, cancellationToken);
        }
    }
}
