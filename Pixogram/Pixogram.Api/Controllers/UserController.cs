using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.UserDtos;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("check")]

        public async Task<IActionResult> Checkuser(string email, string phone)
        {
            var user = await userService.CheckUserAsyc(email, phone);
            return Ok(user);
        }

        [HttpPost("profile/update")]
        public async Task<IActionResult> UpdateProfile(UserUpdateDto userUpdateDto)
        {
            var user = userService.UpdateUserAsync(userUpdateDto, GetUserId());
            return Ok(user);
        }
        protected string GetUserId()
        {
            var identy = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = identy.FindFirst("userid")?.Value;
            return userId;
        }
    }
}
