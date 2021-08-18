using LoanCalculator.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Dtos
{
    public class SubmissionViewModel
    {
        public long Id { get; set; }

        public int Terms { get; set; }

        public double LoanAmount { get; set; }

        public double InterestRate { get; set; }

        public LoanStatusEnum Status { get; set; }

        public long CustomerId { get; set; }

        public string CustomerFullName { get; set; }
    }
}
