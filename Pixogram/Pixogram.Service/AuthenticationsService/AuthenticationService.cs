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
        public async Task<ServiceResponse<string>> LoginAsync(string Email, string Password)
        {
            ServiceResponse<string> response = new();
            var users = await _userRepository.GetByEmail(Email);
            if (users == null)
            {
                throw new UnauthorizedException("No user found with this name");
            }
            if (!_userExtensionService.CheckIfUserPasswordIsCorrect(Password, users.Password))
            {
                throw new UnauthorizedException("Incorrect password");
            }
            var usr =  _userExtensionService.GenerateUserAccessToken(users);
            response.Data = usr.Bearer;
            response.SuccessCode = 200;
            return response;
        }
    }
}
