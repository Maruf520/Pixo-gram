using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.CommentDtos;
using Pixogram.Service.CommentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            this.commentService = commentService;
            this.httpContextAccessor = httpContextAccessor;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateComment( CreateComment createCommentDto)
        {
            try 
            {
                var userId = GetUserId();
                var comment = await commentService.CreateCommentAsync(createCommentDto.postid, createCommentDto.commentbody, userId);
                return Ok(comment);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

        }


        protected string GetUserId()
        {
            var identy = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = identy.FindFirst("userid")?.Value;
            return userId;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(string id)
        {
            var comments = await commentService.GetCommentById(id);
            return Ok(comments);
        }
    }
}
