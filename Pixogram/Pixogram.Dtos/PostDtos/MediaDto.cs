using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.PostDtos
{
    public class MediaDto
    {
        public List<IFormFile> image { get; set; }
        public string location { get; set; }
        public string postbody { get; set; }
        
    }
}
