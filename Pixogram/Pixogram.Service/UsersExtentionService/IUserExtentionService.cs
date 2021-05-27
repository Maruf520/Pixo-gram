using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.UsersExtentionService
{
    public interface IUserExtentionService
    {
        string GetUserHashPassword(string password);
        bool CheckIfUserPasswordIsCorrect(string password, string hashpassword);
        TokenDto GenerateUserAccessToken(User user);
    }
}
