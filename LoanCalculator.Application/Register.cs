using LoanCalculator.Application.Calculation.Interfaces;
using LoanCalculator.Application.Calculation.Services;
using LoanCalculator.Application.Customer.Commands;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Application.Customer.Services;
using LoanCalculator.Application.PaymentSchedule.Commands;
using LoanCalculator.Application.PaymentSchedule.Interfaces;
using LoanCalculator.Application.PaymentSchedule.Services;
using LoanCalculator.Application.Quote.Commands;
using LoanCalculator.Application.Quote.Interfaces;
using LoanCalculator.Application.Quote.Services;
using LoanCalculator.Application.Submission.Commands;
using LoanCalculator.Application.Submission.Interfaces;
using LoanCalculator.Application.Submission.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanCalculator.Application
{
    public static class Register
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services,
                                                                     IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerCommand, PostCustomerCommand>();
            services.AddScoped<ISubmissionService, SubmissionService>();
            services.AddScoped<ISubmissionCommand, PostSubmissionCommand>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IQuoteCommand, PostQuoteCommand>();
            services.AddScoped<IPaymentScheduleService, PaymentScheduleService>();
            services.AddScoped<IPaymentScheduleCommand, PostPaymentScheduleCommand>();
            services.AddScoped<ICalculationService, CalculationService>();
            services.AddScoped<IPostQuoteCommand, Calculation.Commands.PostQuoteCommand>();
            services.AddMediatR(typeof(CustomerService).Assembly);
            services.AddMediatR(typeof(SubmissionService).Assembly);
            services.AddMediatR(typeof(QuoteService).Assembly);
            services.AddMediatR(typeof(PaymentScheduleService).Assembly);
            services.AddMediatR(typeof(CalculationService).Assembly);
            return services;
        }
    }
}
