using LoanCalculator.Application.PaymentSchedule.Dtos;
using LoanCalculator.Application.PaymentSchedule.Interfaces;
using LoanCalculator.Application.PaymentSchedule.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Handlers
{
    public class GetPaymentScheduleByIdHandler : IRequestHandler<GetPaymentScheduleByIdQuery, PaymentScheduleViewModel>
    {
        private readonly IPaymentScheduleService _paymentScheduleService;

        public GetPaymentScheduleByIdHandler(IPaymentScheduleService paymentScheduleService)
        {
            _paymentScheduleService = paymentScheduleService;
        }

        public async Task<PaymentScheduleViewModel> Handle(GetPaymentScheduleByIdQuery request,
                                                           CancellationToken cancellationToken)
        {
            return await _paymentScheduleService.GetPaymentScheduleByIdAsync(request.Id, cancellationToken);
        }
    }
}
