using LoanCalculator.Common;
using LoanCalculator.Core.Entities;
using LoanCalculator.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanCalculator.Persistence.Context
{
    public class LoanCalculatorDBContextSeed
    {
        public static async Task SeedAsync(ILoanCalculatorDBContext dbContext,
                                           ILoggerFactory loggerFactory, 
                                           int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                if (!await dbContext.Customers.AnyAsync())
                {
                    await dbContext.Customers.AddRangeAsync(CreateInitialCustomers());
                    await dbContext.SaveAsync();
                }

                if (!await dbContext.Submissions.AnyAsync())
                {
                    await dbContext.Submissions.AddRangeAsync(CreateInitialSubmissions(await dbContext.Customers.FirstOrDefaultAsync()));
                    await dbContext.SaveAsync();
                }

                if (!await dbContext.Quotes.AnyAsync())
                {
                    await dbContext.Quotes.AddRangeAsync(CreateInitialQuotes(await dbContext.Submissions.FirstOrDefaultAsync()));
                    await dbContext.SaveAsync();
                }

                /*if (!await dbContext.PaymentSchedules.AnyAsync())
                {
                    await dbContext.PaymentSchedules.AddRangeAsync(CreateInitialPaymentSchedule(await dbContext.Quotes.FirstOrDefaultAsync()));
                    await dbContext.SaveAsync();
                }*/
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<LoanCalculatorDBContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<Customer> CreateInitialCustomers()
        {
            Helper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

            return new List<Customer>()
            {
                new Customer()
                {
                    Title = "Mr.",
                    FirstName = "Gerald",
                    LastName = "de los Santos",
                    Email = "gerald.delossantos@gmail.com",
                    Mobile = "+639291314476",
                    UserName = "gerald",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
                new Customer()
                {
                    Title = "Ms.",
                    FirstName = "Anne",
                    LastName = "de los Vega",
                    Email = "anneDV@gmail.com",
                    Mobile = "+639292345678",
                    UserName = "anne",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            };
        }

        static IEnumerable<Submission> CreateInitialSubmissions(Customer customer)
        {
            return new List<Submission>()
            {
                new Submission()
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Terms = 10,
                    InterestRate = 5,
                    LoanAmount = 20000,
                    DateCreated = DateTime.UtcNow,
                    Status = Core.Common.LoanStatusEnum.Submitted
                }
            };
        }

        static IEnumerable<Quote> CreateInitialQuotes(Submission submission)
        {
            var calculation = new QuoteCalculation(DateTime.UtcNow);

            return new List<Quote>()
            {
                new Quote()
                {
                    Submission = submission,
                    SubmissionId = submission.Id,
                    Term = submission.Terms,
                    InterestRate = submission.InterestRate,
                    LoanAmount = calculation.LoanAmount,
                    MonthlyRepayment = calculation.MonthlyRepayment,
                    DateCreated = DateTime.UtcNow,
                    LoanDate = calculation.LoanDate
                }
            };
        }

        static IEnumerable<PaymentSchedule> CreateInitialPaymentSchedule(Quote quote)
        {
            var currentBaseAmount = quote.LoanAmount;
            var rn = (quote.InterestRate / 100) / 12;
            int numberOfPayment = quote.Term * 12;
            var monthlyPayment = quote.LoanAmount * (rn / (1 - Math.Pow(1 + rn, -numberOfPayment)));
            var paymentDate = DateTime.UtcNow;
            var montlyPaymentSchedule = new List<PaymentSchedule>();
            var totalInterest = 0D;

            for (int n = 1; n <= numberOfPayment; n++)
            {
                var interest = currentBaseAmount * rn;
                totalInterest += interest;
                var principal = monthlyPayment - interest;
                currentBaseAmount -= principal;
                paymentDate = paymentDate.AddMonths(1);
                var payment = new PaymentSchedule()
                {
                    Quote = quote,
                    QuoteId = quote.Id,
                    DateCreated = DateTime.UtcNow,
                    PaymentDate = paymentDate,
                    Payment = Math.Round(monthlyPayment, 2),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    TotalInterest = Math.Round(totalInterest, 2),
                    Balance = Math.Round(currentBaseAmount, 2)
                };
                montlyPaymentSchedule.Add(payment);
            }

            return montlyPaymentSchedule;
        }
    }
}
