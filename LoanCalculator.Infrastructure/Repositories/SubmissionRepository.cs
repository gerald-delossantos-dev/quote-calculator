using LoanCalculator.Core.Entities;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(ILoanCalculatorDBContext dbContext) : base(dbContext)
        { }
    }
}
