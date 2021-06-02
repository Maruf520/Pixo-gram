using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.UsersService
{
    public interface IUserService
    {
        Task<ServiceResponseForSignup<int>> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<ServiceResponse<int>> CheckUserAsyc(string email,string phone);
        Task<ServiceResponse<string>> UpdateUserAsync(UserUpdateDto user, string userid);
    }
}
