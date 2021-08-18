using LoanCalculator.Core.Common;
using System;

namespace LoanCalculator.Core.Entities
{
    public class PaymentSchedule : BaseEntity
    {
        public double Payment { get; set; }

        public double Principal { get; set; }

        public double Interest { get; set; }

        public double TotalInterest { get; set; }

        public double Balance { get; set; }

        public DateTime PaymentDate { get; set; }

        public long QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}
