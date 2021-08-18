using LoanCalculator.Application.Calculation.Dtos;
using LoanCalculator.Application.Calculation.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoanCalculator.API.Controllers
{
    [EnableCors("UserPolicy")]
    public class CalculateQuoteController : Controller
    {
        private readonly IPostQuoteCommand _quoteCommand;

        public CalculateQuoteController(IPostQuoteCommand quoteCommand)
        {
            _quoteCommand = quoteCommand;
        }

        [HttpPost]
        [Route("quote-submission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] QuoteSubmissionPostModel quoteModel)
        {
            try
            {
                var newQuoteSubmission = await _quoteCommand.Create(quoteModel);

                return StatusCode(StatusCodes.Status201Created, newQuoteSubmission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
