using AutoMapper;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<Advertisement,AdvertisementListDto>().ReverseMap();
            CreateMap<Advertisement,AdvertisementCreateDto>().ReverseMap();
            CreateMap<Advertisement,AdvertisementUpdateDto>().ReverseMap();
        }
    }
}
