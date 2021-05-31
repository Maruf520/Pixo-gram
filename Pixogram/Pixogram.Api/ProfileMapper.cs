using AutoMapper;
using Pixogram.Dtos.PostDtos;
using Pixogram.Dtos.UserDtos;
using Pixogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixogram.Api
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, GetPostDto>();
            CreateMap<GetPostDto, Post>();
        }
    }
}
