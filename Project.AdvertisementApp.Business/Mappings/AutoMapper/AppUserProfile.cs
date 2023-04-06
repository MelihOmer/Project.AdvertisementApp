using AutoMapper;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AppUserProfile :Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserCreateDto>().ReverseMap();
            CreateMap<AppUser, AppUserListDto>().ReverseMap();
            CreateMap<AppUser, AppUserUpdateDto>().ReverseMap();
        }
    }
}
