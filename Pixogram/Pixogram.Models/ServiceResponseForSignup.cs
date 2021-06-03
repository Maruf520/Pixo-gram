using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class ServiceResponseForSignup<T>
    {
        public ServiceResponseForSignup()
        {
            Errors = new List<string>();
        }

        public bool Success { get; set; } = true;
        public int SuccessCode { get; set; }
        public T Data { get; set; }

        public string Message { get; set; } = null;
        public string Username { get; set; }
        public string OTP { get; set; }
        public List<string> Errors { get; set; }

    }
}
