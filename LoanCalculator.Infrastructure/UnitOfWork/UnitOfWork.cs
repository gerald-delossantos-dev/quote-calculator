using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILoanCalculatorDBContext _dbContext;
        public ICustomerRepository CustomerRepository { get; set; }
        public ISubmissionRepository SubmissionRepository { get; set; }
        public IQuoteRepository QuoteRepository { get; set; }
        public IPaymentScheduleRepository PaymentScheduleRepository { get; set; }

        public UnitOfWork(ILoanCalculatorDBContext dbContext,
                          ICustomerRepository customerRepository,
                          ISubmissionRepository submissionRepository,
                          IQuoteRepository quoteRepository,
                          IPaymentScheduleRepository paymentScheduleRepository)
        {
            _dbContext = dbContext;
            CustomerRepository = customerRepository;
            SubmissionRepository = submissionRepository;
            QuoteRepository = quoteRepository;
            PaymentScheduleRepository = paymentScheduleRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveAsync();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveAsync(cancellationToken);
        }
    }
}
