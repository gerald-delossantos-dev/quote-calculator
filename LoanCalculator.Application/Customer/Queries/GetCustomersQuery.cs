using LoanCalculator.Application.Customer.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LoanCalculator.Application.Customer.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerViewModel>>
    { }
}
