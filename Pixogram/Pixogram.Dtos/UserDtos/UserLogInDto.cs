using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.UserDtos
{
    public class UserLogInDto
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}
