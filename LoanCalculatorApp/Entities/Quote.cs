using LoanCalculator.Core.Common;
using System;

namespace LoanCalculator.Core.Entities
{
    public class Quote : BaseEntity
    {
        public double LoanAmount { get; set; }
        public double MonthlyRepayment { get; set; }
        public double InterestRate { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime StartDate { get; set; }

        public int Term { get; set; }

        public long SubmissionId { get; set; }

        public Submission Submission { get; set; }
    }
}
