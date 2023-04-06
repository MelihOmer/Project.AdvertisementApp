using AutoMapper;
using FluentValidation;
using Project.AdvertisementApp.Business.Extensions;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Common;
using Project.AdvertisementApp.DataAccess.UnitOfWork;
using Project.AdvertisementApp.Dtos.Interfaces;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto ,T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().CreateAsync(createdEntity);
                await _uow.SaveChangeAsync();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(dto, errors: result.ConvertCustomValidationError ());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {

            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {

            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{id} Nolu Kayıt Bulunamadı");
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> Remove(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{id} Nolu Kayıt Bulunamadı");
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangeAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<T>().FindAsync(dto.Id);
                if (unchangedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} Nolu Kayıt Bulunamadı");
                }
                var data = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(data, unchangedData);
                await _uow.SaveChangeAsync();
                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(dto, result.ConvertCustomValidationError());

          
        }
    }
}
