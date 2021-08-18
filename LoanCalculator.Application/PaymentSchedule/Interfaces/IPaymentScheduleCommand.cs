using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Interfaces
{
    public interface IPaymentScheduleCommand
    {
        Task<bool> Delete(long id);
    }
}
