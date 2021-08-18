using LoanCalculator.Application.Submission.Dtos;
using LoanCalculator.Application.Submission.Interfaces;
using LoanCalculator.Core.Common;
using LoanCalculator.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<SubmissionService> _logger;

        public SubmissionService(IUnitOfWork unitOfWork,
                                 IAppLogger<SubmissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SubmissionViewModel> GetSubmissionByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(id, cancellationToken);

            return new SubmissionViewModel
            {
                Id = submission.Id,
                Terms = submission.Terms,
                CustomerFullName = "Test Test",
                LoanAmount = submission.LoanAmount,
                CustomerId = submission.CustomerId,
                InterestRate = submission.InterestRate,
                Status = submission.Status
            };
        }

        public async Task<IEnumerable<SubmissionViewModel>> GetSubmissionsAsync(CancellationToken cancellationToken = default)
        {
            var submisssions = await _unitOfWork.SubmissionRepository.ListAllAsync(cancellationToken);

            return submisssions.Select(submission => new SubmissionViewModel
            {
                Id = submission.Id,
                Terms = submission.Terms,
                CustomerFullName = "Test Test",
                LoanAmount = submission.LoanAmount,
                CustomerId = submission.CustomerId,
                InterestRate = submission.InterestRate,
                Status = submission.Status
            });
        }

        public async Task<SubmissionViewModel> CreateSubmissionAsync(SubmissionPostModel createModel, CancellationToken cancellationToken = default)
        {
            var submisson = await _unitOfWork.SubmissionRepository.AddAsync(new Core.Entities.Submission()
            {
                Terms = createModel.Terms,
                LoanAmount = createModel.LoanAmount,
                Status = LoanStatusEnum.Submitted,
                InterestRate = createModel.InterestRate,
                CustomerId = createModel.CustomerId,
                DateCreated = System.DateTime.UtcNow
            }, cancellationToken);

            if (await _unitOfWork.SaveAsync(cancellationToken) > 0)
            {
                return new SubmissionViewModel
                {
                    Terms = submisson.Terms,
                    LoanAmount = submisson.LoanAmount,
                    Status = submisson.Status,
                    InterestRate = submisson.InterestRate,
                    CustomerId = submisson.CustomerId
                };
            }

            return null;
        }

        public async Task<bool> UpdateSubmissionAsync(SubmissionPostModel updateModel, CancellationToken cancellationToken = default)
        {
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(updateModel.Id, cancellationToken);

            if (submission != null)
            {
                submission.Terms = updateModel.Terms;
                submission.LoanAmount = updateModel.LoanAmount;
                submission.Status = updateModel.Status;
                submission.InterestRate = updateModel.InterestRate;
                submission.CustomerId = updateModel.CustomerId;
                submission.DateLastModified = System.DateTime.UtcNow;

                await _unitOfWork.SubmissionRepository.UpdateAsync(submission, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteSubmissionAsync(long id, CancellationToken cancellationToken = default)
        {
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(id, cancellationToken);

            if (submission != null)
            {
                await _unitOfWork.SubmissionRepository.DeleteAsync(submission, cancellationToken);
            }

            return await _unitOfWork.SaveAsync(cancellationToken) > 0;
        }
    }
}
