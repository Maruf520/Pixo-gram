using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.LikeRepositories
{
    public interface ILikeRepository
    {
        Task<ServiceResponse<string>> CreateAsync(Like like, string postid);
        Task<bool> GetById(string userId, string postId);
    }
}
