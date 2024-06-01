﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Common.DTO.RegisterUser;
using Services.UserModule.Commands.RegisterUser;
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
            if(string.IsNullOrEmpty(authType))
            {
                return BadRequest();
            }
            if(authType == "credential")
            {
                var loginRequest = mapper.Map<LoginUserRequest>(userRequest);

               var token = await Login(loginRequest);

                return Ok(token);
            }
         
            if(authType == "register")
            {
                var registerResult = await Register(userRequest);

                return registerResult ? Ok() : BadRequest();
            }

            return BadRequest();
        }

        private async Task<string> Login(LoginUserRequest loginRequest)
        {
            var loginUserCommand = mapper.Map<LoginUserQuery>(loginRequest);

            var token = await sender.Send(loginUserCommand);

            return token;
        }

        private async Task<bool> Register(RegisterUserRequest regiterRequest)
        {
            var registerUserCommand = mapper.Map<RegisterUserCommand>(regiterRequest);

            return await sender.Send(registerUserCommand);
        }
    }
}
