using Pixogram.Models;
using Pixogram.Repository.LikeRepositories;
using Pixogram.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.LikeServices
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository likeRepository;
        private readonly IUserRepository userRepository;
        public LikeService(ILikeRepository likeRepository, IUserRepository userRepository)
        {
            this.likeRepository = likeRepository;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<string>> CreateLikeAsync(string userId, string postId)
        {
            ServiceResponse<string> response = new();
            var user = await userRepository.GetById(userId);
            /*            if (likeRepository.GetById(userId, postId).Result == false)
                        {*/
            User user1 = new User
            {
                Id = user.Id,
                UserFullName = user.UserFullName,
                UserName = user.UserName,
                Email = user.Email,
                UserProfileImage = user.UserProfileImage,
                Password = ""
            };
            Like like = new();
            /*like.UserName = user.UserName;*/
            /*                like.UserId = userId;
                        like.PostId = postId;*/
            /*like.UserProfileImage = "no image";*/
            like.User = user1;
                var createLike = await likeRepository.CreateAsync(like, postId);
                response.Success = true;
                response.SuccessCode = 200;
                response.Message = "Successfull";
                return response;
/*            }
            response.Success = false;
            response.Message = "You already liked.";
            response.SuccessCode = 500;
            return response;*/
        }
    }
}
