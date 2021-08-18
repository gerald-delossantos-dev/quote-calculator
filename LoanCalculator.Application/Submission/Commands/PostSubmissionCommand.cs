using LoanCalculator.Application.Submission.Dtos;
using LoanCalculator.Application.Submission.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Commands
{
    public class PostSubmissionCommand : ISubmissionCommand
    {
        private const int httpTimeout = 10000; // todo: move to config
        private readonly ISubmissionService _submissionService;

        public PostSubmissionCommand(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        public async Task<SubmissionViewModel> Create(SubmissionPostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            var newSubmission = await _submissionService.CreateSubmissionAsync(model, cancellationTokenSource.Token);

            return newSubmission;
        }

        public async Task<bool> Update(SubmissionPostModel model)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _submissionService.UpdateSubmissionAsync(model, cancellationTokenSource.Token);
        }

        public async Task<bool> Delete(long id)
        {
            var cancellationTokenSource = new CancellationTokenSource(httpTimeout);

            return await _submissionService.DeleteSubmissionAsync(id, cancellationTokenSource.Token);
        }
    }
}
