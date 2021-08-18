using LoanCalculator.Application.PaymentSchedule.Dtos;
using LoanCalculator.Application.PaymentSchedule.Interfaces;
using LoanCalculator.Application.PaymentSchedule.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoanCalculator.API.Controllers
{
    public class PaymentScheduleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPaymentScheduleCommand _paymentScheduleCommand;

        public PaymentScheduleController(IMediator mediator,
                                         IPaymentScheduleCommand paymentScheduleCommand)
        {
            _mediator = mediator;
            _paymentScheduleCommand = paymentScheduleCommand;
        }

        [HttpGet]
        [Route("paymentschedules")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quoteListViewModel = await _mediator.Send(new GetPaymentSchedulesQuery());

                return Ok(quoteListViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("paymentschedules/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var quoteViewModel = await _mediator.Send(new GetPaymentScheduleByIdQuery(id));

                if (quoteViewModel == null) return NotFound("Payment Schedule does not exist");

                return Ok(quoteViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("paymentschedules/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var isDeleted = await _paymentScheduleCommand.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
