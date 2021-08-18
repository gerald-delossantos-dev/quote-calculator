using LoanCalculator.Application.PaymentSchedule.Dtos;
using LoanCalculator.Application.PaymentSchedule.Interfaces;
using LoanCalculator.Application.PaymentSchedule.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.PaymentSchedule.Handlers
{
    public class GetPaymentSchedulesHandler : IRequestHandler<GetPaymentSchedulesQuery, IEnumerable<PaymentScheduleViewModel>>
    {
        private readonly IPaymentScheduleService _paymentScheduleeService;

        public GetPaymentSchedulesHandler(IPaymentScheduleService paymentScheduleeService)
        {
            _paymentScheduleeService = paymentScheduleeService;
        }

        public async Task<IEnumerable<PaymentScheduleViewModel>> Handle(GetPaymentSchedulesQuery request,
                                                                        CancellationToken cancellationToken)
        {
            return await _paymentScheduleeService.GetPaymentSchedulesAsync(cancellationToken);
        }
    }
}
