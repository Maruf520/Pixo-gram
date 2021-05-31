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
        public async Task<ServiceResponse<int>> CreateUserAsync(string Username, string Email, string Phone, string Password)
        {
            ServiceResponse<int> response = new();
            
            if(Email != null)
            {
                var existsuser = userRepository.GetByEmail(Email);
                if (existsuser.Result != null)
                {
                    response.Success = false;
                    response.Message = "Email Already Exists.";

                    return response;
                }

            }
            else if(Username != null)
            {
                if (userRepository.GetByName(Username).Result != null)
                {
                    response.Success = false;
                    response.Message = "Userame already exists.";
                    return response;
                }
            }
            else if(Phone != null)
            {
               if (userRepository.GetByPhone(Phone).Result != null)
                {
                    response.Success = false;
                    response.Message = "Phone number already exists.";
                    return response;
                }
            }
            UserRegisterDto userTOCreate = new();
                userTOCreate.Password = userExtentionService.GetUserHashPassword(Password);
                userTOCreate.Email = Email;
                userTOCreate.UserName = Username;
                userTOCreate.Phone = Phone;
                var createUser = await userRepository.CreateAsync(userTOCreate);
                var createdUser = mapper.Map<GetUserDto>(createUser);

                response.Success = true;
                response.Message = "User Registered Successfully";
                return response;
        }

        public async Task<ServiceResponse<int>> CheckUserAsyc(string email, string phone)
        {
            ServiceResponse<int> response = new();
            var userbyemail = await userRepository.GetByEmail(email);
            var userbyPhone = await userRepository.GetByPhone(phone);
            if (email != null && phone == null)
            {
                if (userbyemail != null)
                {
                    response.Message = "This email already axists";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
                else
                {
                    response.Message = "This email is available";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
            }


            else if (phone != null && email == null)
            {
                if (userbyPhone == null)
                {
                    response.Message = "This phone is avaiable";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
                else
                {
                    response.Message = "This phone is already exitst.";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }

            }
            else
            {
                if (userbyemail == null && userbyPhone == null)
                {
                    response.Message = " phone & email avaiable";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
                else if (userbyemail == null && userbyPhone != null)
                {
                    response.Message = " phone already exists.";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
                else if(userbyemail != null && userbyPhone == null)
                {
                    response.Message = " email already exists.";
                    response.Success = false;
                    response.SuccessCode = 200;
                    return response;
                }
            }
            
             
            response.Message = "something went wrong";
            response.Success = true;
            response.SuccessCode = 200;
            return response;
        }

       
    }
}
