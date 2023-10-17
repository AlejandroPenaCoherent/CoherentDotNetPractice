using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPractice.Controllers
{
    [Route("api/OrderDetails")]
    [ApiController]
    [Authorize]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService ordersService)
        {
            _orderDetailsService = ordersService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(OrderDetail orderDetail)
        {
            var result = await _orderDetailsService.Create(orderDetail);

            if (result.Succeded)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpGet("Read")]
        public async Task<ActionResult> Read(int orderId, int productId)
        {
            var result = await _orderDetailsService.Read(orderId, productId);

            if (result.Succeded && result.Data != null)
            {
                return Ok(result.Data);
            }

            return NotFound();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(OrderDetail orderDetail)
        {
            var result = await _orderDetailsService.Update(orderDetail);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int orderId, int productId)
        {
            var result = await _orderDetailsService.Delete(orderId, productId);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }
    }
}
