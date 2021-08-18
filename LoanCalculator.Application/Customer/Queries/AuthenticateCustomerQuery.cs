using LoanCalculator.Application.Customer.Dtos;
using MediatR;

namespace LoanCalculator.Application.Customer.Queries
{
    public class AuthenticateCustomerQuery : IRequest<CustomerViewModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AuthenticateCustomerQuery(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
