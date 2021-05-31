using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.CommentRepositories
{
    public interface ICommentRepository
    {
        Task<Post> CreateAsync(Comment post);
    }
}
