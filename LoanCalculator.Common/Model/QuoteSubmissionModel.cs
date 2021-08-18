using System;

namespace LoanCalculator.Common.Model
{
    public class QuoteSubmissionModel
    {
        public QuoteSubmissionModel(DateTime? loanDate, double loanAmount = 20000D, double interestRate = 5D, int term = 2)
        {
            if (!loanDate.HasValue) loanDate = DateTime.UtcNow;

            LoanDate = loanDate.Value;
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            Term = term;
        }

        public DateTime LoanDate { get; set; }

        public double LoanAmount { get; set; }

        public int Term { get; set; }

        public double InterestRate { get; set; }
    }
}
