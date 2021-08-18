using LoanCalculator.Application.Calculation.Dtos;
using LoanCalculator.Application.Calculation.Interfaces;
using LoanCalculator.Common;
using LoanCalculator.Common.Model;
using LoanCalculator.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Calculation.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<CalculationService> _logger;

        public CalculationService(IUnitOfWork unitOfWork,
                                  IAppLogger<CalculationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<QuoteSubmissionViewModel> CreateQuoteSubmissionAsync(QuoteSubmissionPostModel createModel, CancellationToken cancellationToken = default)
        {
            createModel.LoanDate = System.DateTime.UtcNow;

            var customer = await CreateCustomerAsync(createModel, cancellationToken);

            var submission = await CreateSubmissionAsync(createModel, customer, cancellationToken);

            var calculation = new QuoteCalculation(new QuoteSubmissionModel(createModel.LoanDate, createModel.LoanAmount, createModel.InterestRate, createModel.Term));
            var firstPayment = calculation.PaymentSchedules.FirstOrDefault();
            var totalInterestPaid = calculation.PaymentSchedules.LastOrDefault().TotalInterest;
            createModel.StartDate = firstPayment.PaymentDate;

            var quote = await CreateQuoteAsync(createModel, submission, calculation, cancellationToken);


            if (await _unitOfWork.SaveAsync(cancellationToken) > 0)
            {
                return new QuoteSubmissionViewModel
                {
                    Id = quote.Id,
                    Title = customer.Title,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Mobile = customer.Mobile,
                    Email = customer.Email,
                    LoanDate = quote.LoanDate,
                    LoanAmount = quote.LoanAmount,
                    Term = quote.Term,
                    InterestRate = quote.InterestRate,
                    PaymentStartDate = quote.StartDate,
                    MonthlyRepayment = firstPayment.Payment,
                    TotalInterestPaid = totalInterestPaid,
                    PaymentSchedules = calculation.PaymentSchedules
                };
            }

            return null;
        }

        #region private methods

        private async Task<Core.Entities.Customer> CreateCustomerAsync(QuoteSubmissionPostModel createModel, CancellationToken cancellationToken = default)
        {
            var customer = await _unitOfWork.CustomerRepository.AddAsync(new Core.Entities.Customer()
            {
                Title = createModel.Title,
                FirstName = createModel.FirstName,
                LastName = createModel.LastName,
                Mobile = createModel.Mobile,
                Email = createModel.Email,
                DateCreated = createModel.LoanDate
            }, cancellationToken);

            return customer;
        }

        private async Task<Core.Entities.Submission> CreateSubmissionAsync(QuoteSubmissionPostModel createModel, 
                                                                         Core.Entities.Customer customer, 
                                                                         CancellationToken cancellationToken = default)
        {
            var submission = await _unitOfWork.SubmissionRepository.AddAsync(new Core.Entities.Submission()
            {
                Customer = customer,
                CustomerId = customer.Id,
                Terms = createModel.Term,
                InterestRate = createModel.InterestRate,
                LoanAmount = createModel.LoanAmount,
                DateCreated = createModel.LoanDate,
                Status = Core.Common.LoanStatusEnum.Submitted
            }, cancellationToken);

            return submission;
        }

        private async Task<Core.Entities.Quote> CreateQuoteAsync(QuoteSubmissionPostModel createModel,
                                                                 Core.Entities.Submission submission,
                                                                 QuoteCalculation calculation,
                                                                 CancellationToken cancellationToken = default)
        {
            var quote = await _unitOfWork.QuoteRepository.AddAsync(new Core.Entities.Quote()
            {
                Submission = submission,
                SubmissionId = submission.Id,
                LoanDate = createModel.LoanDate,
                DateCreated = createModel.LoanDate,
                LoanAmount = createModel.LoanAmount,
                MonthlyRepayment = calculation.MonthlyRepayment,
                InterestRate = createModel.InterestRate,
                Term = createModel.Term,
                StartDate = createModel.StartDate
            }, cancellationToken);

            await CreatePaymentScheduleAsync(createModel, quote, calculation.PaymentSchedules, cancellationToken);

            return quote;
        }

        private async Task CreatePaymentScheduleAsync(QuoteSubmissionPostModel createModel,
                                                      Core.Entities.Quote quote,
                                                      List<MonthlyPaymentScheduleModel> PaymentSchedules,
                                                      CancellationToken cancellationToken = default)
        {
            var paymentSchedules = PaymentSchedules.Select(x => new Core.Entities.PaymentSchedule
            {
                Quote = quote,
                QuoteId = quote.Id,
                Balance = x.Balance,
                Payment = x.Payment,
                PaymentDate = x.PaymentDate,
                Interest = x.Interest,
                TotalInterest = x.TotalInterest,
                Principal = x.Principal,
                DateCreated = createModel.LoanDate
            });

            await _unitOfWork.PaymentScheduleRepository.AddRangeAsync(paymentSchedules, cancellationToken);
        }

        #endregion
    }
}
