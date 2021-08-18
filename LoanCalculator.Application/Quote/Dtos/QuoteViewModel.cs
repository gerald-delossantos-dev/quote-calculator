using System;

namespace LoanCalculator.Application.Quote.Dtos
{
    public class QuoteViewModel
    {
        public long Id { get; set; }

        public double LoanAmount { get; set; }

        public double MonthlyRepayment { get; set; }

        public double InterestRate { get; set; }

        public DateTime StartDate { get; set; }

        public int Term { get; set; }

        public long SubmissionId { get; set; }
    }
}
