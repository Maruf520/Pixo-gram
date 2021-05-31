using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.CommentDtos
{
    public class GetCommentDto
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string UserProfilePic { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PostId { get; set; }
    }
}
