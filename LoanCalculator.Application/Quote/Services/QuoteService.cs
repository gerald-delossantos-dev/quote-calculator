using LoanCalculator.Application.Quote.Dtos;
using LoanCalculator.Application.Quote.Interfaces;
using LoanCalculator.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoanCalculator.Common;

namespace LoanCalculator.Application.Quote.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<QuoteService> _logger;

        public QuoteService(IUnitOfWork unitOfWork,
                            IAppLogger<QuoteService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<QuoteViewModel> GetQuoteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var quote = await _unitOfWork.QuoteRepository.GetByIdAsync(id, cancellationToken);

            return new QuoteViewModel
            {
                Id = quote.Id,
                LoanAmount = quote.LoanAmount,
                Term = quote.Term,
                StartDate = quote.StartDate
            };
        }

        public async Task<IEnumerable<QuoteViewModel>> GetQuotesAsync(CancellationToken cancellationToken = default)
        {
            var quotes = await _unitOfWork.QuoteRepository.ListAllAsync(cancellationToken);

            return quotes.Select(quote => new QuoteViewModel
            {
                Id = quote.Id,
                LoanAmount = quote.LoanAmount,
                Term = quote.Term,
                StartDate = quote.StartDate
            });
        }

        public async Task<QuoteViewModel> CreateQuoteAsync(QuotePostModel createModel, CancellationToken cancellationToken = default)
        {
            var loanDate = System.DateTime.UtcNow;
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(createModel.SubmissionId, cancellationToken);

            var calculation = new QuoteCalculation(loanDate, createModel.LoanAmount, createModel.InterestRate, createModel.Term);

            var quote = await _unitOfWork.QuoteRepository.AddAsync(new Core.Entities.Quote()
            {
                Submission = submission,
                SubmissionId = submission.Id,
                LoanDate = loanDate,
                DateCreated = loanDate,
                LoanAmount = createModel.LoanAmount,
                MonthlyRepayment = calculation.MonthlyRepayment,
                InterestRate = createModel.InterestRate,
                Term = createModel.Term
            }, cancellationToken);

            var paymentSchedules = calculation.PaymentSchedules.Select(x => new Core.Entities.PaymentSchedule
            {
                Quote = quote,
                QuoteId = quote.Id,
                Balance = x.Balance,
                Payment = x.Payment,
                PaymentDate = x.PaymentDate,
                Interest = x.Interest,
                TotalInterest = x.TotalInterest,
                Principal = x.Principal,
                DateCreated = loanDate
            });

            await _unitOfWork.PaymentScheduleRepository.AddRangeAsync(paymentSchedules, cancellationToken);

            if (await _unitOfWork.SaveAsync(cancellationToken) > 0)
            {
                return new QuoteViewModel
                {
                    Id = quote.Id,
                    LoanAmount = quote.LoanAmount,
                    Term = quote.Term
                };
            }

            return null;
        }

        public async Task<bool> UpdateQuoteAsync(QuotePostModel updateModel, CancellationToken cancellationToken = default)
        {
            var quote = await _unitOfWork.QuoteRepository.GetByIdAsync(updateModel.Id, cancellationToken);

            if (quote != null)
            {
                quote.LoanAmount = updateModel.LoanAmount;
                quote.Term = updateModel.Term;
                quote.StartDate = System.DateTime.UtcNow;

                await _unitOfWork.QuoteRepository.UpdateAsync(quote, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteQuoteAsync(long id, CancellationToken cancellationToken = default)
        {
            var quote = await _unitOfWork.QuoteRepository.GetByIdAsync(id, cancellationToken);

            if (quote != null)
            {
                await _unitOfWork.QuoteRepository.DeleteAsync(quote, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }
    }
}
