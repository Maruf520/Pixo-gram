﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pixogram.Dtos.PostDtos;
using Pixogram.Service.PostServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pixogram.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PostController(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            this.postService = postService;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected string GetUserId()
        {
            var identy = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = identy.FindFirst("userid")?.Value;
            return userId;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePost([FromForm] MediaDto files)
        {
            List<string> medias = new List<string>();

            foreach (var formFile in files.image)
            {
                if (formFile.Length > 0)
                {
                    string fName = Path.GetRandomFileName();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Flush();

                        medias.Add(filePath);
                    }
                }
            }
            CreatePostDto createPostDto = new CreatePostDto();
            createPostDto.Medias = medias;
            createPostDto.location = files.location;
            createPostDto.postbody = files.postbody;
            var postTocreate = await postService.CreatePostAsync(createPostDto, GetUserId());
            return Ok(postTocreate);
        }

        [HttpGet("/{id}/media")]
        public async Task<IActionResult> DownloadPostMedia(string id)
        {
            var postMedia = await postService.DownloadMediaAsync(id);
            return Ok(postMedia);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await postService.GetPostById(id);
            return Ok(post);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPost()
        {
            var allpost = postService.GetAllPosts();
            return Ok(allpost);
        }
    }
}