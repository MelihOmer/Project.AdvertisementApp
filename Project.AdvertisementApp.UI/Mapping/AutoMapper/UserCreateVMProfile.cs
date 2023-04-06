using AutoMapper;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.UI.Models;

namespace Project.AdvertisementApp.UI.Mapping.AutoMapper
{
    public class UserCreateVMProfile :Profile
    {
        public UserCreateVMProfile()
        {
            CreateMap<UserCreateVM, AppUserCreateDto>();
        }
    }
}
