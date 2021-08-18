using Ardalis.Specification;
using LoanCalculator.Core.Entities;
using System;
using System.Linq;

namespace LoanCalculator.Core.Specification
{
    public class CustomerSpecification : Specification<Customer>
    {
        public CustomerSpecification(string username)
        {
            Query.Where(c => username.Equals(c.UserName));
        }
    }
}
