using LoanCalculator.Application.PaymentSchedule.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Interfaces
{
    public interface IPaymentScheduleService
    {
        Task<PaymentScheduleViewModel> GetPaymentScheduleByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentScheduleViewModel>> GetPaymentSchedulesAsync(CancellationToken cancellationToken = default);
        Task<bool> DeletePaymentScheduleAsync(long id, CancellationToken cancellationToken = default);
    }
}
