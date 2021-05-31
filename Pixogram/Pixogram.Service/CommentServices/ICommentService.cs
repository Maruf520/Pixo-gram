using Pixogram.Dtos.CommentDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.CommentServices
{
    public interface ICommentService
    {
        Task<Post> CreateCommentAsync(string Postid, string comment, string userid);
    }
}
