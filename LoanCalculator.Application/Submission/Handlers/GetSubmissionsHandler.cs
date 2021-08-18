using LoanCalculator.Application.Submission.Dtos;
using LoanCalculator.Application.Submission.Interfaces;
using LoanCalculator.Application.Submission.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Application.Submission.Handlers
{
    public class GetSubmissionsHandler : IRequestHandler<GetSubmissionsQuery, IEnumerable<SubmissionViewModel>>
    {
        private readonly ISubmissionService _submissionService;

        public GetSubmissionsHandler(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        public async Task<IEnumerable<SubmissionViewModel>> Handle(GetSubmissionsQuery request,
                                                    CancellationToken cancellationToken)
        {
            return await _submissionService.GetSubmissionsAsync(cancellationToken);
        }
    }
}
