using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public IFormFile image { get; set; }
    }
}
