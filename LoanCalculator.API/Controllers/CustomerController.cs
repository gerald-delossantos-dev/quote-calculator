using LoanCalculator.API.Models.Users;
using LoanCalculator.APIKeys;
using LoanCalculator.Application.Customer.Dtos;
using LoanCalculator.Application.Customer.Interfaces;
using LoanCalculator.Application.Customer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculator.API.Controllers
{
    //[Authorize]
    //[ApiController]
    [EnableCors("UserPolicy")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerCommand _customerCommand;

        public CustomerController(IMediator mediator, 
                                  ICustomerCommand customerCommand)
        {
            _mediator = mediator;
            _customerCommand = customerCommand;
        }

        [AllowAnonymous]
        [HttpPost("customers/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var customerViewModel = await _mediator.Send(new AuthenticateCustomerQuery(model.Username, model.Password));

            if (customerViewModel == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ApiKey.QuoteSubmissionApiKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(type: ClaimTypes.Name, value: customerViewModel.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(ApiKey.ApiKeyExpiryInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = customerViewModel.Id,
                Username = customerViewModel.UserName,
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                Token = tokenString
            });
        }

        [HttpGet]
        [Route("customers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customerListViewModel = await _mediator.Send(new GetCustomersQuery());

                return Ok(customerListViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var customerViewModel = await _mediator.Send(new GetCustomerByIdQuery(id));

                if (customerViewModel == null) return NotFound("Customer does not exist");

                return Ok(customerViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("customers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CustomerPostModel customerModel)
        {
            try
            {
                var newCustomer = await _customerCommand.Create(customerModel);

                return StatusCode(StatusCodes.Status201Created, newCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("customers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] CustomerPostModel customerModel)
        {
            try
            {
                var isUpdated = await _customerCommand.Update(customerModel);

                return StatusCode(StatusCodes.Status204NoContent, isUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var isDeleted = await _customerCommand.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
