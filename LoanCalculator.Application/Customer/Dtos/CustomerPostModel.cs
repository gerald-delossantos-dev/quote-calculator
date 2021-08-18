using System;

namespace LoanCalculator.Application.Customer.Dtos
{
    public class CustomerPostModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
