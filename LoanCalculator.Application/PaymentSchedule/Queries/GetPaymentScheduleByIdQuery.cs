using LoanCalculator.Application.PaymentSchedule.Dtos;
using MediatR;

namespace LoanCalculator.Application.PaymentSchedule.Queries
{
    public class GetPaymentScheduleByIdQuery : IRequest<PaymentScheduleViewModel>
    {
        public long Id { get; set; }

        public GetPaymentScheduleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
