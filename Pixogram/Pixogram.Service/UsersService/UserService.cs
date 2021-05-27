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
        public async Task<ServiceResponse<GetUserDto>> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            ServiceResponse<GetUserDto> response = new();
            var userTOCreate = userRegisterDto;
            userTOCreate.Password = userExtentionService.GetUserHashPassword(userTOCreate.Password);
            var createUser = await userRepository.CreateAsync(userTOCreate);
            var createdUser = mapper.Map<GetUserDto>(createUser);
            response.Data = createdUser;
            response.Success = true;
            response.Message = "User Registered Successfully";
            return response;
        }
    }
}
