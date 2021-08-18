using LoanCalculator.Application.Submission.Dtos;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Interfaces
{
    public interface ISubmissionCommand
    {
        Task<SubmissionViewModel> Create(SubmissionPostModel model);
        Task<bool> Update(SubmissionPostModel model);
        Task<bool> Delete(long id);
    }
}
