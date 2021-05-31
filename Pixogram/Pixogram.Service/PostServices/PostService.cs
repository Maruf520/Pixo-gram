using AutoMapper;
using Pixogram.Dtos.PostDtos;
using Pixogram.Models;
using Pixogram.Repository.PostsRepository;
using Pixogram.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.PostServices
{
    public class PostService : IPostService
    {
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;
        public PostService(IUserRepository userRepository, IMapper mapper, IPostRepository postRepository)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }
        public async Task<GetPostDto> CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            var comments = new List<Comment>();
            var likes = new List<Like>();
            var user = await userRepository.GetById(userId);
            
            var postToCreate = mapper.Map<Post>(createPostDto);
            postToCreate.UserName = user.UserName;
            postToCreate.UserId = user.Id;
            postToCreate.CreatedAt = DateTime.Now;
            postToCreate.Comments = comments;
            postToCreate.likes = likes;
            var post =   postRepository.Create(postToCreate);
            var createdPost = mapper.Map<GetPostDto>(post);

            return post;


        }

        public async Task<ServiceResponseForMedia<string>> DownloadMediaAsync(string id)
        {
            ServiceResponseForMedia<string> response = new ServiceResponseForMedia<string>();
            var post = await postRepository.GetbyId(id);
            if (post != null)
            {
                List<string> medias = new List<string>();
                PostMediaDto postMediaDto = new();
                medias = null;
                medias = post.Medias;
                postMediaDto.Media = medias;
                response.Data = postMediaDto.Media;
                response.SuccessCode = 200;

                return response;
            }
            response.Success = false;
            response.Message = "Post Not Found";
            response.SuccessCode = 204;
            return response;

        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();
            var allpost = postRepository.GetAll();
            posts = allpost;
            return posts;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(string id)
        {

            ServiceResponse<GetPostDto> response = new();
            var post = await postRepository.GetbyId(id);

            if(post == null)
            {
                response.Success = false;
                response.Message = "Post Not Found";
                response.SuccessCode = 204;
                return response;
            }
            var postToReturn = mapper.Map<GetPostDto>(post);
            response.Data = postToReturn;
            response.Success = true;
            response.SuccessCode = 200;
            return response;
        }

    }
}
