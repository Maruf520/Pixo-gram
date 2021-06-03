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
        Task<GetUserDto> CreateAsync(UserRegisterDto userRegisterDto, string trimmedemail);
        Task<User> GetByEmail(string email);
        Task<User> GetByName(string name);
        Task<User> GetByPhone(string phone);
        Task<User> GetById(string id);
        bool Getbyphonebool(string phone);
        bool GetByEmailbool(string email);
        bool GetByUserbool(string username);
        Task<User> UpdateAsync(User user, string UserId);
        bool CheckTrimmedEmail(string username);
    }
}
