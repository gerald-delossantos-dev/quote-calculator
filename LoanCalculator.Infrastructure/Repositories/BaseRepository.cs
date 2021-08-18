using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using LoanCalculator.Core.Common;
using LoanCalculator.Infrastructure.Interfaces;
using LoanCalculator.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ILoanCalculatorDBContext _dbContext;

        public BaseRepository(ILoanCalculatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _dbContext.SetEntity<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SetEntity<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.SetEntity<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.SetEntity<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.EntityEntry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.SetEntity<T>().Remove(entity);
        }

        public async Task<T> AuthenticateAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            try
            {
                var specificationResult = ApplySpecification(spec);
                var user = await specificationResult.FirstAsync(cancellationToken);
                return user;
            }
            catch (Exception ex)
            {

            }
            return default;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var evaluator = new SpecificationEvaluator<T>();
            return evaluator.GetQuery(_dbContext.SetEntity<T>().AsQueryable(), spec);
        }
    }
}
