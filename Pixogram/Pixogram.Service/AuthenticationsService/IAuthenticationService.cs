using Pixogram.Dtos.Token;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Service.AuthenticationsService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<TokensDto>> LoginAsync(string userid, string Password);
    }
}
