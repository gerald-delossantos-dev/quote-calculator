using System;

namespace LoanCalculator.Common.Model
{
    public class MonthlyPaymentScheduleModel
    {
        public DateTime PaymentDate { get; set; }

        public double Payment { get; set; }

        public double Principal { get; set; }

        public double Interest { get; set; }

        public double TotalInterest { get; set; }

        public double Balance { get; set; }
    }
}
