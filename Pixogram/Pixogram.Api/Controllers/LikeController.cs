using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Service.LikeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public LikeController(ILikeService likeService, IHttpContextAccessor httpContextAccessor)
        {
            this.likeService = likeService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create/postid/{id}")]
        public async Task<IActionResult> CreateReact(string id)
        {
            var react = await likeService.CreateLikeAsync(GetUserId(), id);
            return Ok(react);
        }

        protected string GetUserId()
        {
            var identy = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = identy.FindFirst("userid")?.Value;
            return userId;
        }
    }
}
