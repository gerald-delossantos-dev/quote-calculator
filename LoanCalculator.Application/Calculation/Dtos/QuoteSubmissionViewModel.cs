using LoanCalculator.Common;
using LoanCalculator.Common.Model;
using System;
using System.Collections.Generic;

namespace LoanCalculator.Application.Calculation.Dtos
{
    public class QuoteSubmissionViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public double LoanAmount { get; set; }
        public double MonthlyRepayment { get; set; }
        public double TotalInterestPaid { get; set; }
        public DateTime PaymentStartDate { get; set; }
        public double TotalCost { 
            get
            {
                return LoanAmount + TotalInterestPaid;
            } 
        }

        public List<MonthlyPaymentScheduleModel> PaymentSchedules { get; set; }
    }

}
