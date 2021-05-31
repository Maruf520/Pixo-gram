using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.PostDtos
{
    public class CreatePostDto
    {
        public string postbody { get; set; }
        public string  location { get; set; }
        public List<string> Medias { get; set; }
    }
}
