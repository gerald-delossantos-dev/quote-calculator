using LoanCalculator.Core.Common;
using LoanCalculator.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Persistence.Interfaces
{
    public interface ILoanCalculatorDBContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Submission> Submissions { get; set; }
        DbSet<Quote> Quotes { get; set; }
        DbSet<PaymentSchedule> PaymentSchedules { get; set; }

        DbSet<T> SetEntity<T>() where T : BaseEntity;

        EntityEntry<T> EntityEntry<T>([NotNull] T entity) where T : BaseEntity;

        int Save();

        Task<int> SaveAsync();

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
