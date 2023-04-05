using Project.AdvertisementApp.Common;
using Project.AdvertisementApp.Dtos.Interfaces;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.Business.Interfaces
{
    public interface IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T :BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto);

        Task<IResponse<IDto>> GetByIdAsync<IDto>(int id);

        Task<IResponse> Remove(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync();
    }
}
