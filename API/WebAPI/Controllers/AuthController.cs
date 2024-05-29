using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authenticate(RegisterUserRequest regiterRequest, string authType)
        {
            if(string.IsNullOrEmpty(authType))
            {
                return BadRequest();
            }
            if(authType == "credential")
            {
                var loginRequest = mapper.Map<LoginUserRequest>(regiterRequest);

                await Login(loginRequest);
            }
         
            if(authType == "register")
            {
                await Register(regiterRequest);
            }

            return BadRequest();
        }

        private async Task<IActionResult> Login(LoginUserRequest regiterRequest)
        {
            return Ok();
        }

        private async Task<IActionResult> Register(RegisterUserRequest regiterRequest)
        {
            return Ok();
        }
    }
}
