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
        Task<ServiceResponse<string>> CreateCommentAsync(string Postid, string comment, string userid);
        Task<ServiceResponse<List<Comment>>> GetCommentById (string postId);
    }
}
