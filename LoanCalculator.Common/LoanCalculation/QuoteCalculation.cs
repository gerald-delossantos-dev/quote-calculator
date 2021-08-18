using LoanCalculator.Common.Model;
using System;
using System.Collections.Generic;

namespace LoanCalculator.Common
{
    public class QuoteCalculation
    {
        public QuoteCalculation()
        {
            LoanAmount = 20000D;
            InterestRate = 5D;
            Term = 2;

            CalculateMonthlyPayment();
        }

        public QuoteCalculation(QuoteSubmissionModel quoteSubmission)
        {
            LoanDate = quoteSubmission.LoanDate;
            LoanAmount = quoteSubmission.LoanAmount;
            InterestRate = quoteSubmission.InterestRate;
            Term = quoteSubmission.Term;

            CalculateMonthlyPayment();
        }

        public QuoteCalculation(DateTime? loanDate, double loanAmount = 20000D, double interestRate = 5D, int term = 2)
        {
            if (!loanDate.HasValue) loanDate = DateTime.UtcNow;

            LoanDate = loanDate.Value;
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            Term = term;

            CalculateMonthlyPayment();
        }

        private const int monthsInAYear = 12;

        private double rn
        {
            get
            {
                return (InterestRate / 100) / monthsInAYear;
            }
        }

        private double numberOfPayment
        {
            get
            {
                return Term * monthsInAYear;
            }
        }

        public DateTime LoanDate { get; set; }

        public double LoanAmount { get; set; }

        public int Term { get; set; }

        public double InterestRate { get; set; }

        public double MonthlyRepayment { 
            get
            {
                return LoanAmount * (rn / (1 - Math.Pow(1 + rn, -numberOfPayment)));
            }
        }

        public List<MonthlyPaymentScheduleModel> PaymentSchedules { get; set; }

        public void CalculateMonthlyPayment()
        {
            var currentBaseAmount = LoanAmount;
            var paymentDate = LoanDate;
            var montlyPayment = new List<MonthlyPaymentScheduleModel>();
            var totalInterest = 0D;

            for (int n = 1; n <= numberOfPayment; n++)
            {
                var interest = currentBaseAmount * rn;
                totalInterest += interest;
                var principal = MonthlyRepayment - interest;
                currentBaseAmount -= principal;
                paymentDate = paymentDate.AddMonths(1);
                var payment = new MonthlyPaymentScheduleModel()
                {
                    PaymentDate = paymentDate,
                    Payment = Math.Round(MonthlyRepayment, 2),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    TotalInterest = Math.Round(totalInterest, 2),
                    Balance = Math.Round(currentBaseAmount, 2)
                };
                montlyPayment.Add(payment);
            }

            PaymentSchedules = montlyPayment;
        }
    }
}
