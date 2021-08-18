using LoanCalculator.Core.Entities;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class PaymentScheduleRepository : BaseRepository<PaymentSchedule>, IPaymentScheduleRepository
    {
        public PaymentScheduleRepository(ILoanCalculatorDBContext dbContext) : base(dbContext)
        { }
    }
}
