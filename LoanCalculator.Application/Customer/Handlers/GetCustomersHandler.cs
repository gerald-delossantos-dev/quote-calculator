using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Application.Customer.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Customer.Handlers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerViewModel>> Handle(GetCustomersQuery request,
                                                                 CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomersAsync(cancellationToken);
        }
    }
}
