using Pixogram.Dtos.OtpDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.OtpRepositories
{
    public interface IotpRepository
    {
        Task<TempData> CreateOtp(CreateOtpDto createOtpDto);
        Task<string> RemoveOtp(string email, int Otp);
        bool CheckOtpIsExists(string email, int Otp);
    }
}
