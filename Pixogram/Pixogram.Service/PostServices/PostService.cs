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
        public async Task<ServiceResponse<string>> CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            ServiceResponse<string> response = new();
            var comments = new List<Comment>();
            var likes = new List<Like>();
            var user = await userRepository.GetById(userId);
            
            var postToCreate = mapper.Map<Post>(createPostDto);
            postToCreate.UserName = user.UserName;
            postToCreate.UserId = user.Id;
            postToCreate.CreatedAt = DateTime.Now;
            postToCreate.Comments = comments;
            postToCreate.likes = likes;
            postToCreate.User = user;
            var post =   postRepository.Create(postToCreate);
            response.Data = "";
            response.Success = true;
            response.Message = "Post Created";
            response.SuccessCode = 200;
            return response;


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

        public async Task<ServiceResponse<List<Post>>> GetAllPosts()
        {
            ServiceResponse<List<Post>> response = new();
            List<Post> postss = new List<Post>();

            var allpost = postRepository.GetAll().ToList();
            postss = allpost;

            response.Data = postss;
            response.Success = true;
            response.Message = "all posts";
            response.SuccessCode = 200;
            return response;
        }

        public async Task<ServiceResponse<Post>> GetPostById(string id)
        {

            ServiceResponse<Post> response = new();
            Post posts = new();
            var post = await postRepository.GetbyId(id);
            posts = post;
            if(post == null)
            {
                response.Success = false;
                response.Message = "Post Not Found";
                response.SuccessCode = 204;

                return response;
            }
            response.Data = posts;
            response.Success = true;
            response.SuccessCode = 200;
            return response;
        }

        public async Task<ServiceResponse<List<Post>>> GetPostUserById(string id)
        {
            /*            ServiceResponse<Post> response = new();
                        List<Post> postss = new List<Post>();
                        postss = null;
                        GetPostDto postDto = new();
                        var allpost = postRepository.GetbyUserId(id).ToList();
                        postss = allpost;
                        postDto.posts = postss;
                        response.Data = postDto;
                        response.Success = true;
                        response.Message = "all posts";
                        response.SuccessCode = 200;
                        return response;*/

            ServiceResponse<List<Post>> response = new();
            List<Post> postss = new List<Post>();

            var allpost = postRepository.GetbyUserId(id).ToList();
            postss = allpost;

            response.Data = postss;
            response.Success = true;
            response.Message = "all posts";
            response.SuccessCode = 200;
            return response;
        }

    }
}
