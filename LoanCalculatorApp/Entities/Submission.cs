using LoanCalculator.Core.Common;

namespace LoanCalculator.Core.Entities
{
    public class Submission : BaseEntity
    {
        public int Terms { get; set; }

        public double LoanAmount { get; set; }

        public double InterestRate { get; set; }

        public LoanStatusEnum Status { get; set; }


        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
