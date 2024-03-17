using AutoMapper;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.dto;

namespace UserServices
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<UserDto, User>();
            CreateMap<UserGetDto, User>();
            CreateMap<User, UserGetDto>();
        }
    }
}
