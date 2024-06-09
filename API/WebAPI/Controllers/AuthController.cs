using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Common.DTO.RegisterUser;
using Services.UserModule.Commands.RegisterUser;
using Services.UserModule.Commands.UpdatePassword;
using Services.UserModule.Commands.UpdateUsername;
using Services.UserModule.Queries.LoginUser;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMapper mapper, ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authenticate(RegisterUserRequest userRequest, string authType)
        {
            if (string.IsNullOrEmpty(authType))
            {
                return BadRequest();
            }
            if (authType == "credential")
            {
                var loginRequest = mapper.Map<LoginUserRequest>(userRequest);

                var result = await Login(loginRequest);

                return Ok(result);
            }

            if (authType == "register")
            {
                var registerResult = await Register(userRequest);

                return registerResult ? Ok() : BadRequest();
            }

            return BadRequest();
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> ChangeUsername(string userId, [FromQuery] string username)
        {
            var updateUsernameCommand = new UpdateUsernameRequest(userId, username);

            var usernameChanged = await sender.Send(updateUsernameCommand);

            if (usernameChanged)
            {
                return Ok(username);
            }
            return BadRequest();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> ChangePassword(string userId, [FromQuery] string oldPassword, [FromQuery]string newPassword)
        {
            var updatePasswordCommand = new UpdatePasswordRequest(userId, oldPassword, newPassword);

            var passwordChanged = await sender.Send(updatePasswordCommand);

            if (passwordChanged)
            {
                return Ok();
            }
            return BadRequest();
        }

        private async Task<string> Login(LoginUserRequest loginRequest)
        {
            var loginUserCommand = mapper.Map<LoginUserQuery>(loginRequest);

            var result = await sender.Send(loginUserCommand);

            return result;
        }

        private async Task<bool> Register(RegisterUserRequest regiterRequest)
        {
            var registerUserCommand = mapper.Map<RegisterUserCommand>(regiterRequest);

            return await sender.Send(registerUserCommand);
        }
    }
}
