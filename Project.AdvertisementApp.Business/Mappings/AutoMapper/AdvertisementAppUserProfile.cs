using AutoMapper;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementAppUserProfile :Profile
    {
        public AdvertisementAppUserProfile()
        {
            CreateMap<AdvertisementAppUser, AdvertisementAppUserCreateDto>().ReverseMap();
            CreateMap<AdvertisementAppUser,AdvertisementAppUserListDto>().ReverseMap();
        }
    }
}
