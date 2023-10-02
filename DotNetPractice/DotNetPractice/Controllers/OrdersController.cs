using DotNetPractice.Backend.Services;
using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPractice.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Order order)
        {
            var result = await _ordersService.Create(order);

            if (result.Succeded)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpGet("Read/{orderId}")]
        public async Task<ActionResult> Read(int orderId)
        {
            var result = await _ordersService.Read(orderId);

            if (result.Succeded && result.Data != null)
            {
                return Ok(result.Data);
            }

            return NotFound();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(Order order)
        {
            var result = await _ordersService.Update(order);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpDelete("Delete/{OrderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {
            var result = await _ordersService.Delete(orderId);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }
    }
}
