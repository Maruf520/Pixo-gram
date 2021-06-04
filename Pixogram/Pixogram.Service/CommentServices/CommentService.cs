using AutoMapper;
using Pixogram.Dtos.CommentDtos;
using Pixogram.Models;
using Pixogram.Repository.CommentRepositories;
using Pixogram.Repository.PostsRepository;
using Pixogram.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public CommentService(IUserRepository userRepository, IPostRepository postRepository, IMapper mapper, ICommentRepository commentRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
        }
        public async Task<ServiceResponse<string>> CreateCommentAsync(string postid, string comments, string userid)
        {
            var user = await userRepository.GetById(userid);
            var post = await postRepository.GetbyId(postid);
            ServiceResponse<string> response = new();
            Comment Comments = new Comment()
            {
                Message = comments,
                CreatedAt = DateTime.Now,
                User = user,
                Id = "60b9b16a6c44594bcd3a6999"
                   
            };
/*            Post post1 = new();
            post1.Comments = ;*/

            var comment = await  commentRepository.CreateAsync(Comments, postid);
            response.Message = "Successfull";
            response.Success = true;
            response.SuccessCode = 200;
            return response;
        }
        public async Task<ServiceResponse<List<Comment>>> GetCommentById(string postId)
        {
            ServiceResponse<List<Comment>> response = new();
            if(postId == "")
            {
                response.Success = false;
                response.SuccessCode = 404;
                response.Message = "Please enter post id.";
                return response;
            }
            if (postRepository.GetbyId(postId) == null)
            {
                response.Success = false;
                response.SuccessCode = 404;
                response.Message = "Post Not Found.";
                return response;
            }
            /*
                        ServiceResponse<List<Post>> response = new();
                        List<Post> postss = new List<Post>();

                        var allpost = postRepository.GetbyUserId(id).ToList();
                        postss = allpost;

                        response.Data = postss;
                        response.Success = true;
                        response.Message = "all posts";
                        response.SuccessCode = 200;
                        return response;*/

            List<Comment> comments = new();
           
            var commentList = await commentRepository.GetById(postId);
            comments = commentList;
            
            if(commentList.Count() == 0)
            {
                response.Data = comments;
                response.Success = false;
                response.SuccessCode = 404;
                response.Message = "No Comment";
                return response;
            }
            response.Data = comments;
            response.Success = false;
            response.SuccessCode = 200;
            return response;

        }
    }
}
