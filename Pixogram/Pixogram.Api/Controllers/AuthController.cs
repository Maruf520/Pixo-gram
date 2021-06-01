
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using Pixogram.Service.AuthenticationsService;
using Pixogram.Service.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthController(IUserService userService, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            _authenticationService = authenticationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = await userService.CreateUserAsync(userRegisterDto.username, userRegisterDto.email,userRegisterDto.phone,userRegisterDto.password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> LoginAsync(UserLogInDto userLogInDto)
        {

            var token = await _authenticationService.LoginAsync(userLogInDto.phone, userLogInDto.email, userLogInDto.password);

            return Ok(token);
        }


    }
}
