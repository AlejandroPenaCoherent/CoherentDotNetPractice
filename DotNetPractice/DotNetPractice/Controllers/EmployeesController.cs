using DotNetPractice.Backend.Services.Interfaces;
using DotNetPractice.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPractice.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Employee employee)
        {
            var result = await _employeesService.Create(employee);
            
            if (result.Succeded)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpGet("Read/{employeeId}")]
        public async Task<ActionResult> Read(int employeeId)
        {
            var result = await _employeesService.Read(employeeId);

            if (result.Succeded && result.Data != null)
            {
                return Ok(result.Data);
            }

            return NotFound(result.ContatenatedErrors);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(Employee employee)
        {
            var result = await _employeesService.Update(employee);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }

        [HttpDelete("Delete/{employeeId}")]
        public async Task<ActionResult> Delete(int employeeId)
        {
            var result = await _employeesService.Delete(employeeId);

            if (result.Succeded)
            {
                return NoContent();
            }

            return BadRequest(result.ContatenatedErrors);
        }
    }
}
