﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.UserDtos
{
    public class GetUserDto
    {
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
