using LoanCalculator.Application.Customer.Dtos;
using MediatR;

namespace LoanCalculator.Application.Customer.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerViewModel>
    {
        public long Id { get; set; }

        public GetCustomerByIdQuery(long id)
        {
            Id = id;
        }
    }
}
