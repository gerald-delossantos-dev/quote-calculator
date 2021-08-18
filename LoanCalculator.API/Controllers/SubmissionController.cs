using LoanCalculator.Application.Submission.Dtos;
using LoanCalculator.Application.Submission.Interfaces;
using LoanCalculator.Application.Submission.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoanCalculator.API.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISubmissionCommand _submissionCommand;

        public SubmissionController(IMediator mediator,
                                    ISubmissionCommand submissionCommand)
        {
            _mediator = mediator;
            _submissionCommand = submissionCommand;
        }

        [HttpGet]
        [Route("submissions")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var submissionListViewModel = await _mediator.Send(new GetSubmissionsQuery());

                return Ok(submissionListViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("submissions/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var submissionViewModel = await _mediator.Send(new GetSubmissionByIdQuery(id));

                if (submissionViewModel == null) return NotFound("Submission does not exist");

                return Ok(submissionViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("submissions")]
        public async Task<IActionResult> Post([FromBody] SubmissionPostModel submissionModel)
        {
            try
            {
                var newCustomer = await _submissionCommand.Create(submissionModel);

                return StatusCode(StatusCodes.Status201Created, newCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("submissions")]
        public async Task<IActionResult> Put([FromBody] SubmissionPostModel submissionModel)
        {
            try
            {
                var isUpdated = await _submissionCommand.Update(submissionModel);

                return StatusCode(StatusCodes.Status204NoContent, isUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("submissions/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var isDeleted = await _submissionCommand.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
