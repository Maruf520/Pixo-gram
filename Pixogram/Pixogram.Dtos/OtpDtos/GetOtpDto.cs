using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Dtos.OtpDtos
{
    public class GetOtpDto
    {
        public string Email { get; set; }
        public int OTP { get; set; }
    }
}
