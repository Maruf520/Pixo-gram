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
        public async Task<Post> CreateCommentAsync(string postid, string comments, string userid)
        {
            var user = await userRepository.GetById(userid);
            var post = await postRepository.GetbyId(postid);

            Comment Comments = new Comment()
            {
                   UserName = user.UserName,
                   Message = comments,
                   CreatedAt = DateTime.Now,
                   PostId = post.Id,
                   UserProfilePic = "No Pic"  
            };

            var comment = await  commentRepository.CreateAsync(Comments);
            return comment;
        }
    }
}
