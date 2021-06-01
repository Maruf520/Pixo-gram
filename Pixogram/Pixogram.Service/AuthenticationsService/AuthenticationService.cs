using Microsoft.Azure.ServiceBus;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using Pixogram.Repository.UserRepository;
using Pixogram.Service.UsersExtentionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.AuthenticationsService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserExtentionService _userExtensionService;
        public AuthenticationService(IUserRepository userRepository, IUserExtentionService userExtentionService)
        {
            _userRepository = userRepository;
            _userExtensionService = userExtentionService;
        }
        public async Task<ServiceResponse<string>> LoginAsync(string phone,string Email, string Password)
        {
            ServiceResponse<string> response = new();
            var users = await _userRepository.GetByEmail(Email);
            var userbyphone = await _userRepository.GetByPhone(phone);
            if (phone != null)
            {
                if(userbyphone == null)
                {
                    response.Success = false;
                    response.SuccessCode = 500;
                    response.Message = "No User Found";
                    return response;
                }
                if (!_userExtensionService.CheckIfUserPasswordIsCorrect(Password, userbyphone.Password))
                {
                    response.Success = false;
                    response.SuccessCode = 500;
                    response.Message = "Password incorrect";
                    return response;
                        
                }

                var usertoken = _userExtensionService.GenerateUserAccessToken(userbyphone);
                response.Data = usertoken.Bearer;
                response.SuccessCode = 200;
                response.Message = "Log in Successfull.";
                return response;
            }

            
            if (users == null)
            {
                response.Success = false;
                response.SuccessCode = 500;
                response.Message = "No User Found.";
                return response;
            }
            if (!_userExtensionService.CheckIfUserPasswordIsCorrect(Password, users.Password))
            {
                response.Success = false;
                response.SuccessCode = 500;
                response.Message = "password incorect.";
                return response;
            }
            var usr =  _userExtensionService.GenerateUserAccessToken(users);
            response.Data = usr.Bearer;
            response.SuccessCode = 200;
            response.Message = "Log in Successfull.";
            return response;
        }
    }
}
