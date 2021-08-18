using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; set; }
        ISubmissionRepository SubmissionRepository { get; set; }
        IQuoteRepository QuoteRepository { get; set; }
        IPaymentScheduleRepository PaymentScheduleRepository { get; set; }

        Task<int> SaveAsync();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
