using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Models
{
    public class ServiceResponseForMedia<T>
    {
        public bool Success { get; set; } = true;
        public int SuccessCode { get; set; }
        public List<T> Data { get; set; }

        public string Message { get; set; } = null;

        public string Error { get; set; } = null;
    }
}
