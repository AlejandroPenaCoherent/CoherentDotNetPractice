using DotNetPractice.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetPractice.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("GetToken")]
        public async Task<ActionResult> GetToken()
        {
            var tokenResponse = await _authenticationService.GetToken("read write");

            if (tokenResponse.IsError)
            {
                return BadRequest("Something went wrong while trying to get the token.");
            }

            return Ok(new { tokenResponse.AccessToken, tokenResponse.TokenType, tokenResponse.ExpiresIn, tokenResponse.Scope });
        }
    }
}
