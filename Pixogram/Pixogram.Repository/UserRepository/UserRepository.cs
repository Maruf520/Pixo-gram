using AutoMapper;
using MongoDB.Driver;
using OpenQA.Selenium;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using Pixogram.Models.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixogram.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMapper mapper;
        public UserRepository(IDbClient dbclient, IMapper mapper)
        {
            this.mapper = mapper;
            _user = dbclient.GetUsersCollection();
        }
        public async Task<GetUserDto> CreateAsync(UserRegisterDto userRegisterDto)
        {
            var userToCreate = mapper.Map<User>(userRegisterDto);

            await _user.InsertOneAsync(userToCreate);

            var user = mapper.Map<GetUserDto>(userToCreate);

            return user;

        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await  _user.Find(c => c.Email == email).FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<User> GetByName(string name)
        {
            var user = await _user.Find(x => x.UserName == name).FirstOrDefaultAsync();

            return user;
        }

        public Task<User> GetByPhone(string phone)
        {
            var user = _user.Find(x => x.Phone == phone).FirstOrDefaultAsync();
      
            return user;
        }
    }
}
