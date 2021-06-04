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
        public async Task<ServiceResponseForSignup<int>> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            ServiceResponseForSignup<int> response = new();
            if(userRegisterDto.email == "")
            {
                response.Success = false;
                response.SuccessCode = 500;
                response.Message = "Please enter you email";
                return response;
            }
            if(userRepository.GetByEmailbool(userRegisterDto.email) == true)
            {
                response.Message = "Email already exits.";
                return response;
            }

            /*if(userRegisterDto.email == "" && userRegisterDto.phone == "" && userRegisterDto.username == "")
            {
                response.Message = "Please put at least one field.";
                response.SuccessCode = 500;
                response.Success = false;
                return response;
            }
            if(userRegisterDto.password == "")
            {
                response.Message = "Please set a password to signup.";
                response.SuccessCode = 500;
                response.Success = false;
                return response;
            }
            var userbyphone = userRepository.Getbyphonebool(userRegisterDto.phone);
            *//* var userbyphone = CheckPhone(userRegisterDto.phone);*/

            /*var userbyname = CHeckUserName(userRegisterDto.username);*//*
            var userbyname = userRepository.GetByUserbool(userRegisterDto.username);
            *//*var userbyemail = CheckEmail(userRegisterDto.email);*//*
            var userbyemail = userRepository.GetByEmailbool(userRegisterDto.email);
            Dictionary<string, bool> check = new Dictionary<string, bool>()
            {
                { "Phone number already exists",userbyphone },
                { "Email already exists", userbyemail },
                { "User name already exists", userbyname }
            };

            if(userbyname || userbyphone || userbyemail)
            {
                var exception = check.Where(a => a.Value == true).Select(b=>b.Key).ToList();
                response.Errors = exception;
                response.Message = exception.ElementAt(0);
                return response;
            }
*/
            
            string email = userRegisterDto.email;
            
            string emailToTrim = string.Concat(email.TakeWhile((c) => c != '@'));
            var trimmedEmai = emailToTrim + GenerateRandomNo();
            bool checkUserName = userRepository.CheckTrimmedEmail(trimmedEmai);
            while(checkUserName != true)
            {
                trimmedEmai = trimmedEmai + 1;
                
            }

           UserRegisterDto userTOCreate = new();
                          userTOCreate.password = userExtentionService.GetUserHashPassword(userRegisterDto.password);
                         userTOCreate.email = userRegisterDto.email;
                         userTOCreate.fullname = userRegisterDto.fullname;
                         var createUser = await userRepository.CreateAsync(userTOCreate,trimmedEmai);
                         var createdUser = mapper.Map<GetUserDto>(createUser);
                         response.Success = true;
            response.Username = trimmedEmai;
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
                    response.Success = true;
                    response.SuccessCode = 200;
                    return response;
                }
            }


            else if (phone != null && email == null)
            {
                if (userbyPhone == null)
                {
                    response.Message = "This phone is avaiable";
                    response.Success = true;
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

        public async Task<ServiceResponse<string>> UpdateUserAsync(UserUpdateDto user, string userid, string imagename)
        {
            ServiceResponse<string> response = new();
            var userToUpdate = userRepository.GetById(userid);
            if(user.email == "")
            {
                response.Success = false;
                response.Message = "please enter your mail";
                response.SuccessCode = 403;
                return response;
            }
            if(userToUpdate ==  null)
            {
                response.Message = "User Not Found.";
            }
            if (userRepository.GetByEmailbool(user.email) == true)
            {
                response.Success = false;
                response.Message = "Email already exists.";
                response.SuccessCode = 403;
                return response;
            }
            User user1 = new();
            user1.UserFullName = user.username;
            /*user1.Phone = user.phone;*/
            user1.Email = user.email;
            user1.UserProfileImage = imagename;
            await userRepository.UpdateAsync(user1, userid);
            response.Message = "Updated";
            return response;
        }

        public bool CheckPhone(string phone)
        {
            if(phone == "" || userRepository.Getbyphonebool(phone) == false)
            {
                return false;
            }
            return true;
        }

        public bool CHeckUserName(string username)
        {
            if(username == "" || userRepository.GetByUserbool(username) == false)
            {
                return false;
            }
            return true;
        }
        public bool CheckEmail(string email)
        {
            if(email == "" || userRepository.GetByEmailbool(email) == false)
            {
                return false;
            }
            return true;
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
