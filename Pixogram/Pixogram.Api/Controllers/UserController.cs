using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.UserDtos;
using Pixogram.Service.UsersService;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> UpdateProfile([FromForm]UserUpdateDto userUpdateDto)
        {
            string imagename = "";
            if (userUpdateDto.image.Length > 0)
            {
                string fName = Path.GetRandomFileName();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, fName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await userUpdateDto.image.CopyToAsync(stream);
                    stream.Flush();

                    imagename = filePath;
                }
            }
            var user = await userService.UpdateUserAsync(userUpdateDto, GetUserId(), imagename);
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
