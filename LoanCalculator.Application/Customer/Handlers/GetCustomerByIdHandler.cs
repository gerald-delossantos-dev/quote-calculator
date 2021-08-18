using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Application.Customer.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerByIdQuery request,
                                                    CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomerByIdAsync(request.Id, cancellationToken);
        }
    }
}
