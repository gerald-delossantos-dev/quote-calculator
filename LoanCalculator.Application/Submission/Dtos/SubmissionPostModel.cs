using LoanCalculator.Core.Common;

namespace LoanCalculator.Application.Submission.Dtos
{
    public class SubmissionPostModel
    {
        public long Id { get; set; }

        public int Terms { get; set; }

        public int LoanAmount { get; set; }

        public int InterestRate { get; set; }

        public LoanStatusEnum Status { get; set; }

        public int CustomerId { get; set; }
    }
}
