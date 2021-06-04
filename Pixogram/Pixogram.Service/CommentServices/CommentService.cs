using AutoMapper;
using MongoDB.Bson;
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
        private static Random s_Generator = new Random();
        public async Task<ServiceResponse<string>> CreateCommentAsync(string postid, string comments, string userid)
        {
            var user = await userRepository.GetById(userid);
            var post = await postRepository.GetbyId(postid);
            ServiceResponse<string> response = new();
            var objectid = RandomUniqueHexShuffle(24, 4);
            User user1 = new User
            {
                Id = user.Id,
                UserFullName = user.UserFullName,
                UserName = user.UserName,
                Email = user.Email,
                UserProfileImage = user.UserProfileImage,
                Password = ""
            };
            Comment Comments = new Comment()
            {
                Message = comments,
                CreatedAt = DateTime.Now,
                User = user1,
                Id = objectid[0].ToString().ToLower()
            };

            var comment = await commentRepository.CreateAsync(Comments, postid);
            response.Message = "Successfull";
            response.Success = true;
            response.SuccessCode = 200;
            return response;
        }

        private object[] RandomUniqueHexShuffle(int length, int count)
        {
            {
                HashSet<string> used = new HashSet<string>();

                string[] result = new string[count];

                for (int i = 0; i < result.Length;)
                {
                    string value = string.Concat(Enumerable
                      .Range(0, length)
                      .Select(j => s_Generator.Next(0, 16).ToString("x")));

                    if (used.Add(value))
                        result[i++] = value;
                }

                return result;
            }
        }

        public async Task<ServiceResponse<List<Comment>>> GetCommentById(string postId)
        {
            ServiceResponse<List<Comment>> response = new();
            if (postId == "")
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


            List<Comment> comments = new();

            var commentList = await commentRepository.GetById(postId);
            comments = commentList;

            if (commentList.Count() == 0)
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
