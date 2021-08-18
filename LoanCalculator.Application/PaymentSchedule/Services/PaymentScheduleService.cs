using LoanCalculator.Application.PaymentSchedule.Dtos;
using LoanCalculator.Application.PaymentSchedule.Interfaces;
using LoanCalculator.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Services
{
    public class PaymentScheduleService : IPaymentScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<PaymentScheduleService> _logger;

        public PaymentScheduleService(IUnitOfWork unitOfWork,
                                      IAppLogger<PaymentScheduleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PaymentScheduleViewModel> GetPaymentScheduleByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var payment = await _unitOfWork.PaymentScheduleRepository.GetByIdAsync(id, cancellationToken);

            return new PaymentScheduleViewModel
            {
                QuoteId = payment.QuoteId,
                Balance = payment.Balance,
                Interest = payment.Interest,
                Payment = payment.Payment,
                Principal = payment.Principal,
                PaymentDate = payment.PaymentDate
            };
        }

        public async Task<IEnumerable<PaymentScheduleViewModel>> GetPaymentSchedulesAsync(CancellationToken cancellationToken = default)
        {
            var paymentSchedules = await _unitOfWork.PaymentScheduleRepository.ListAllAsync(cancellationToken);

            return paymentSchedules.Select(payment => new PaymentScheduleViewModel
            {
                QuoteId = payment.QuoteId,
                Balance = payment.Balance,
                Interest = payment.Interest,
                Payment = payment.Payment,
                Principal = payment.Principal,
                PaymentDate = payment.PaymentDate
            });
        }

        public async Task<bool> DeletePaymentScheduleAsync(long id, CancellationToken cancellationToken = default)
        {
            var quote = await _unitOfWork.PaymentScheduleRepository.GetByIdAsync(id, cancellationToken);

            if (quote != null)
            {
                await _unitOfWork.PaymentScheduleRepository.DeleteAsync(quote, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }
    }
}
