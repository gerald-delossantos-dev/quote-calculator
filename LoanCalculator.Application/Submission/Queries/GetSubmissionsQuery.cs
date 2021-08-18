using LoanCalculator.Application.Submission.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LoanCalculator.Application.Submission.Queries
{
    public class GetSubmissionsQuery : IRequest<IEnumerable<SubmissionViewModel>>
    { }
}
