using LoanCalculator.Application.Submission.Dtos;
using MediatR;

namespace LoanCalculator.Application.Submission.Queries
{
    public class GetSubmissionByIdQuery : IRequest<SubmissionViewModel>
    {
        public long Id { get; set; }

        public GetSubmissionByIdQuery(long id)
        {
            Id = id;
        }
    }
}
