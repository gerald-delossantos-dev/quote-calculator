using LoanCalculator.Application.Submission.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Interfaces
{
    public interface ISubmissionService
    {
        Task<SubmissionViewModel> GetSubmissionByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SubmissionViewModel>> GetSubmissionsAsync(CancellationToken cancellationToken = default);
        Task<SubmissionViewModel> CreateSubmissionAsync(SubmissionPostModel createModel, CancellationToken cancellationToken = default);
        Task<bool> UpdateSubmissionAsync(SubmissionPostModel updateModel, CancellationToken cancellationToken = default);
        Task<bool> DeleteSubmissionAsync(long id, CancellationToken cancellationToken = default);
    }
}
