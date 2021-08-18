using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Infrastructure.Logging;
using LoanCalculator.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanCalculator.Infrastructure
{
    public static class Register
    {
        public static IServiceCollection RegisterInfrastructerServices(this IServiceCollection services,
                                                                       IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IPaymentScheduleRepository, PaymentScheduleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
