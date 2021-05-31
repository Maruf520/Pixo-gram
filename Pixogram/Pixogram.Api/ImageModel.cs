using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pixogram.Api
{
    public class ImageModel
    {
        public List<IFormFile> image { get; set; }
        public string Name { get; set; }
        public string location { get; set; }
        public string postbody { get; set; }
    }
}
