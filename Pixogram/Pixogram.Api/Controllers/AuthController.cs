
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using Pixogram.Service.AuthenticationsService;
using Pixogram.Service.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IUserService userService, IAuthenticationService authenticationService)
        {
            this.userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = await userService.CreateUserAsync(userRegisterDto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> LoginAsync(UserLogInDto userLogInDto)
        {


            var token = await _authenticationService.LoginAsync(userLogInDto);

            /*var usertoken = new TokenDto() { Bearer = token.Bearer };*/
            return Ok(token);
        }
    }
}
