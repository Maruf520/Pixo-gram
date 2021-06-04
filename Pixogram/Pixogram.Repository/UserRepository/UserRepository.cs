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
        public async Task<GetUserDto> CreateAsync(UserRegisterDto userRegisterDto, string trimmedemail)
        {
            User userToCreate = new();
            userToCreate.UserFullName = userRegisterDto.fullname;
            
            userToCreate.UserName = trimmedemail;
            userToCreate.Email = userRegisterDto.email;
            userToCreate.UserProfileImage = "";
           /* userToCreate.Phone = "";*/
            userToCreate.Password = userRegisterDto.password;

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

        public async Task<User> GetByPhone(string phone)
        {
            var user = await _user.Find(x => x.UserName == phone).FirstOrDefaultAsync();
      
            return user;
        }

        public async Task<User> GetById(string id)
        {
            var user = await _user.Find(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

/*        public bool Getbyphone(string phone)
        {
            var user = _user.Find(x => x.Phone == phone).FirstOrDefaultAsync();
            if(user.Result == null)
            {
                return false;
            }
            return true;
        }*/

        public async Task<User> UpdateAsync(User user, string UserId)
        {
            var userx = await _user.Find(x => x.Id == UserId).FirstOrDefaultAsync();
            userx.UserFullName = user.UserFullName;
            userx.Email = user.Email;
            userx.UserProfileImage = user.UserProfileImage;
            var userToUpdate =  _user.ReplaceOne(x => x.Id == UserId,userx);
            return user;
        }

        public bool Getbyphonebool(string phone)
        {
            var user = _user.Find(x => x.UserName == phone).FirstOrDefaultAsync();
            if(user.Result == null || phone == "")
            {
                return false;
            }
            return true;
        }

        public bool GetByEmailbool(string email)
        {
            var user = _user.Find(x => x.Email == email).FirstOrDefaultAsync();
            if (user.Result == null || email == "")
            {
                return false;
            }
            return true;
        }

        public bool GetByUserbool(string username)
        {
            var user = _user.Find(x => x.UserName == username).FirstOrDefaultAsync();
            if (user.Result == null || username == "")
            {
                return false;
            }
            return true;
        }

        public bool CheckTrimmedEmail(string username)
        {
            var user = _user.Find(x => x.UserName == username).FirstOrDefaultAsync();
            if(user.Result == null)
            {
                return true;
            }
            return false;
        }
    }
}
