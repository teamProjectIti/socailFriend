using AutoMapper;
using Data.Models.Users;
using ModeDTO.DtoAutoMapper;
using ModeDTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Photo, PhotoDTo>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<registerDto, AppUser>();
        }
    }
}
