using LoanCalculator.Application.Common;
using System;

namespace LoanCalculator.Application.Customer.Dtos
{
    public class CustomerViewModel : IDtoEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CustomerFullName {
            get
            {
                return $"{Title} {FirstName} {LastName}";
            }
        }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DateRegistered {
            get
            {
                return DateCreated.ToString("MMMM dd, yyy");
            }
        }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public override string ToString()
        {
            return $"{Title} {LastName}, {FirstName}";
        }
    }
}
