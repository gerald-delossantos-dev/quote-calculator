using LoanCalculator.Persistence.Context;
using LoanCalculator.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanCalculator.Persistence
{
    public static class Register
    {
        public static IServiceCollection RegisterDatabaseContext(this IServiceCollection services,
                                                                       IConfiguration configuration)
        {
            services.AddDbContext<LoanCalculatorDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ILoanCalculatorDBContext>(option => option.GetService<LoanCalculatorDBContext>());

            return services;
        }
    }
}
