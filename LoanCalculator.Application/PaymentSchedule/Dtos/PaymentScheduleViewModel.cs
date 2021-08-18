using System;

namespace LoanCalculator.Application.PaymentSchedule.Dtos
{
    public class PaymentScheduleViewModel
    {
        public double Payment { get; set; }

        public double Principal { get; set; }

        public double Interest { get; set; }

        public double TotalInterest { get; set; }

        public double Balance { get; set; }

        public DateTime PaymentDate { get; set; }

        public long QuoteId { get; set; }
    }
}
