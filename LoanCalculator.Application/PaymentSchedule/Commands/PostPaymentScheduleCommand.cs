using LoanCalculator.Application.PaymentSchedule.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Commands
{
    public class PostPaymentScheduleCommand : IPaymentScheduleCommand
    {
        private const int httpTimeout = 10000; // todo: move to config
        private readonly IPaymentScheduleService _paymentService;

        public PostPaymentScheduleCommand(IPaymentScheduleService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> Delete(long id)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _paymentService.DeletePaymentScheduleAsync(id, cancellationTokenSource.Token);
        }
    }
}
