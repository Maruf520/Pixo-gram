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

        public  Task<User> GetByEmail(string email)
        {
            var cursor =  _user.Find(c => c.Email == email);
            var user = cursor.FirstOrDefaultAsync();
            if(user == null)
            {
                throw new NotFoundException($"No user exists with name{user}");
            }
            return user;
        }
    }
}
