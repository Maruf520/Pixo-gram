using AutoMapper;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using Pixogram.Repository.UserRepository;
using Pixogram.Service.UsersExtentionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.UsersService
{
    public class UserService : IUserService
    {
        private readonly IUserExtentionService userExtentionService;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public UserService(IUserExtentionService userExtentionService, IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userExtentionService = userExtentionService;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<int>> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            ServiceResponse<int> response = new();
            var userTOCreate = userRegisterDto;
            var existsuser = userRepository.GetByEmail(userRegisterDto.Email);
            if (existsuser.Result != null)
            {
                response.Success = false;
                response.Message = "Email Already Exists.";

                return response;
            }
            else if (userRepository.GetByName(userRegisterDto.UserName).Result != null)
            {
                response.Success = false;
                response.Message = "Userame already exists.";
                return response;
            }
            else if(userRepository.GetByPhone(userRegisterDto.Phone).Result != null)
            {
                response.Success = false;
                response.Message = "Phone number already exists.";
                return response;
            }
            userTOCreate.Password = userExtentionService.GetUserHashPassword(userTOCreate.Password);
            var createUser = await userRepository.CreateAsync(userTOCreate);
            var createdUser = mapper.Map<GetUserDto>(createUser);
            
            response.Success = true;
            response.Message = "User Registered Successfully";
            return response;


        }
    }
}
