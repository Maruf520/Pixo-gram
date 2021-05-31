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
        GetPostDto Create(Post posts);
        Task<Post> GetbyId(string id);
        List<Post> GetAll();
    }
}
