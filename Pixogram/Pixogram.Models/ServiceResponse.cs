using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            Errors = new List<string>();
        }

        public bool Success { get; set; } = true;
        public int SuccessCode { get; set; }
        public T Data { get; set; }
        
        public string Message { get; set; } = null;
        public List<string> Errors { get; set; }
        
        public string Error { get; set; } = null;
    }
}
