using LoanCalculator.Application.PaymentSchedule.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LoanCalculator.Application.PaymentSchedule.Queries
{
    public class GetPaymentSchedulesQuery : IRequest<IEnumerable<PaymentScheduleViewModel>>
    { }
}
