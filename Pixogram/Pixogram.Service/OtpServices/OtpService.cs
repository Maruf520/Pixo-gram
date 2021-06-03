using AutoMapper;
using Pixogram.Dtos.OtpDtos;
using Pixogram.Models;
using Pixogram.Repository.OtpRepositories;
using Pixogram.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.OtpServices
{
    public class OtpService : IOtpServices
    {
        private readonly IotpRepository otpRepository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public OtpService(IotpRepository otpRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.otpRepository = otpRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<GetOtpDto>> CreateOtp(string email)
        {
            ServiceResponse<GetOtpDto> response = new();
            if(userRepository.GetByEmailbool(email) == true)
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            
            }
            var generateotp = GenerateRandomNo();
            CreateOtpDto otpdto = new();
            otpdto.Email = email;
            otpdto.OTP = generateotp;
            var otp = await otpRepository.CreateOtp(otpdto);
            GetOtpDto getOtp = new();

            getOtp.Email = otp.Email;
            getOtp.OTP = otp.OTP;
            response.Success = true;
            response.SuccessCode = 200;
            response.Data = getOtp;
            return response;
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task<ServiceResponse<string>> VerifyOtp(CreateOtpDto createOtpDto)
        {
            ServiceResponse<string> response = new();
            var verify = otpRepository.CheckOtpIsExists(createOtpDto.Email,createOtpDto.OTP);
            if(verify == true)
            {
               await otpRepository.RemoveOtp(createOtpDto.Email, createOtpDto.OTP);
                response.Success = true;
                response.Message = "OTP Verified";
                response.SuccessCode = 200;
                return response;
            }
            response.Success = false;
            response.Message = "Invalid OTP";
            response.SuccessCode = 500;
            return response;
        }
    }
}
