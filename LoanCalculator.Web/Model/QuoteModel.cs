using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculator.Web.Model
{
    public class QuoteModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double LoanAmount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Terms { get; set; }
        public double InterestRate { get; set; }
    }
}
