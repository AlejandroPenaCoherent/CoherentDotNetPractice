using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPractice.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Customer customer)
        {
            var result = await _customersService.Create(customer);

            if (result.Succeded)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpGet("Read/{customerId}")]
        public async Task<ActionResult> Read(string customerId)
        {
            var result = await _customersService.Read(customerId);

            if (result.Succeded && result.Data != null)
            {
                return Ok(result.Data);
            }

            return NotFound(result.ContatenatedErrors);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(Customer customer)
        {
            var result = await _customersService.Update(customer);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpDelete("Delete/{customerId}")]
        public async Task<ActionResult> Delete(string customerId)
        {
            var result = await _customersService.Delete(customerId);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }
    }
}
