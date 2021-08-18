using LoanCalculator.Core.Entities;

namespace LoanCalculator.Infrastructure.Interfaces
{
    public interface IPaymentScheduleRepository : IAsyncRepository<PaymentSchedule>
    { }
}
