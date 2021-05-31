using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.LikeServices
{
    public interface ILikeService
    {
        Task<ServiceResponse<int>> CreateLikeAsync(string userId, string postId);
    }
}
