using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<GetUserDto> CreateAsync(UserRegisterDto userRegisterDto);
        Task<User> GetByEmail(string email);
        Task<User> GetByName(string name);
        Task<User> GetByPhone(string phone);
        Task<User> GetById(string id);
        bool Getbyphone(string phone);
        Task<User> UpdateAsync(UserUpdateDto userUpdateDto, string UserId);
    }
}
