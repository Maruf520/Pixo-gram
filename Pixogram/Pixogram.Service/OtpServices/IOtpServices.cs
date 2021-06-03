using Pixogram.Dtos.OtpDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.OtpServices
{
    public interface IOtpServices
    {
        Task<ServiceResponse<GetOtpDto>> CreateOtp(string otp);
        Task<ServiceResponse<string>> VerifyOtp(CreateOtpDto createOtpDto);
    }
}
