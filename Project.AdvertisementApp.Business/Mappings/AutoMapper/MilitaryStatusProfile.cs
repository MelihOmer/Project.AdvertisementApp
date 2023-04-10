using AutoMapper;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.Business.Mappings.AutoMapper
{
    public class MilitaryStatusProfile:Profile
    {
        public MilitaryStatusProfile()
        {
            CreateMap<MilitaryStatus, MilitaryStatusListDto>().ReverseMap();
        }
    }
}
