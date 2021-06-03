using Pixogram.Dtos.PostDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.PostsRepository
{
    public interface IPostRepository
    {
        Task<Post> Create(Post posts);
        Task<Post> GetbyId(string id);
        List<Post> GetAll();
        List<Post> GetbyUserId(string id);
    }
}
