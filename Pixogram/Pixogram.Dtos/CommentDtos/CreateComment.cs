using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.CommentDtos
{
    public class CreateComment
    {
        public string postid { get; set; }
        public string commentbody { get; set; }
    }
}
