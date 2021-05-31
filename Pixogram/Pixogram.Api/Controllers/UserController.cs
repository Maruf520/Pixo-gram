using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Service.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("check")]

        public async Task<IActionResult> Checkuser(string email, string phone)
        {
            var user = await userService.CheckUserAsyc(email, phone);
            return Ok(user);
        }
    }
}
