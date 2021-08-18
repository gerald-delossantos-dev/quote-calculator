using LoanCalculator.Core.Common;
using LoanCalculator.Core.Entities;
using LoanCalculator.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Persistence.Context
{
    public class LoanCalculatorDBContext : DbContext, ILoanCalculatorDBContext
    {
        public LoanCalculatorDBContext(DbContextOptions<LoanCalculatorDBContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }

        public DbSet<T> SetEntity<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public EntityEntry<T> EntityEntry<T>([NotNull] T entity) where T : BaseEntity
        {
            return base.Entry(entity);
        }

        public int Save()
        {
            return this.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
