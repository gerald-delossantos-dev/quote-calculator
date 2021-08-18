using LoanCalculator.Application.Quote.Dtos;
using LoanCalculator.Application.Quote.Interfaces;
using LoanCalculator.Application.Quote.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoanCalculator.API.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IQuoteCommand _quoteCommand;

        public QuoteController(IMediator mediator,
                               IQuoteCommand quoteCommand)
        {
            _mediator = mediator;
            _quoteCommand = quoteCommand;
        }

        [HttpGet]
        [Route("quotes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quoteListViewModel = await _mediator.Send(new GetQuotesQuery());

                return Ok(quoteListViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("quotes/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var quoteViewModel = await _mediator.Send(new GetQuotesByIdQuery(id));

                if (quoteViewModel == null) return NotFound("Quote does not exist");

                return Ok(quoteViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("quotes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] QuotePostModel quoteModel)
        {
            try
            {
                var newQuote = await _quoteCommand.Create(quoteModel);

                return StatusCode(StatusCodes.Status201Created, newQuote);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("quotes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] QuotePostModel quoteModel)
        {
            try
            {
                var isUpdated = await _quoteCommand.Update(quoteModel);

                return StatusCode(StatusCodes.Status204NoContent, isUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("quotes/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var isDeleted = await _quoteCommand.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
