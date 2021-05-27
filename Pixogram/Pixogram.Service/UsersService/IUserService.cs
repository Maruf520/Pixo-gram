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
        Task<ServiceResponse<GetUserDto>> CreateUserAsync(UserRegisterDto userRegisterDto);
    }
}
