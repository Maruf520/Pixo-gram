using Pixogram.Dtos.PostDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.PostServices
{
    public interface IPostService
    {
        Task<GetPostDto> CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<ServiceResponseForMedia<string>> DownloadMediaAsync(string id);
        Task<ServiceResponse<GetPostDto>> GetPostById(string id);
        List<Post> GetAllPosts();
    }
}
