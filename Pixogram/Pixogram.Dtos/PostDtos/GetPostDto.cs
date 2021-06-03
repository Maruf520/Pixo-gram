using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.PostDtos
{
    public class GetPostDto
    {
        public List<Post> posts { get; set; }
    }
}
