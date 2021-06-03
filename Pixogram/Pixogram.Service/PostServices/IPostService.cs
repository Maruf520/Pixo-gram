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
        Task<ServiceResponse<string>> CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<ServiceResponseForMedia<string>> DownloadMediaAsync(string id);
        Task<ServiceResponse<Post>> GetPostById(string id);
        Task<ServiceResponse<List<Post>>> GetPostUserById(string id);
        Task<ServiceResponse<List<Post>>> GetAllPosts();
        
    }
}
