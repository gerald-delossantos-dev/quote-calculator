using LoanCalculator.Application.Submission.Dtos;
using LoanCalculator.Application.Submission.Interfaces;
using LoanCalculator.Application.Submission.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Handlers
{
    public class GetSubmissionByIdHandler : IRequestHandler<GetSubmissionByIdQuery, SubmissionViewModel>
    {
        private readonly ISubmissionService _submissionService;

        public GetSubmissionByIdHandler(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        public async Task<SubmissionViewModel> Handle(GetSubmissionByIdQuery request,
                                                    CancellationToken cancellationToken)
        {
            return await _submissionService.GetSubmissionByIdAsync(request.Id, cancellationToken);
        }
    }
}
