using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.AdvertisementApp.Business.Extensions;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Common;
using Project.AdvertisementApp.Common.Enums;
using Project.AdvertisementApp.DataAccess.UnitOfWork;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.Business.Services
{
    public class AdvertisementAppUserService : IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createValidator;
        private readonly IMapper _mapper;

        public AdvertisementAppUserService(IUow uow, IValidator<AdvertisementAppUserCreateDto> createValidator, IMapper mapper)
        {
            _uow = uow;
            _createValidator = createValidator;
            _mapper = mapper;
        }

        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
            var result = _createValidator.Validate(dto);
            if (result.IsValid)
            {
                var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.AppUserId == dto.AppUserId && x.AdvertisementId == dto.AdvertisementId);
                if (control == null)
                {
                    var createdAdvertisementUser = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createdAdvertisementUser);
                    await _uow.SaveChangeAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }
                List<CustomValidationError> errors = new List<CustomValidationError>
                {
                    new CustomValidationError
                    {
                        ErrorMessage="İlana Daha Önce Başvurulmuş.",
                        PropertyName=""

                    }
                };
                return new Response<AdvertisementAppUserCreateDto>(dto, errors);
                //
            }
            else
            {
                return new Response<AdvertisementAppUserCreateDto>(dto, result.ConvertCustomValidationError());
            }
        }


        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            var list = await query
                  .Include(x => x.Advertisement)
                  .Include(x => x.AdvertisementAppUserStatus)
                  .Include(x => x.MilitaryStatus)
                  .Include(x => x.AppUser)
                  .ThenInclude(x => x.Gender).Where(x => x.AdvertisementAppUserStatusId == (int)type).ToListAsync();


            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }
        public async Task SetStatusAsync(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            var unchanged = await _uow.GetRepository<AdvertisementAppUser>().FindAsync(advertisementAppUserId);
            var changed = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.Id == advertisementAppUserId);
            changed.AdvertisementAppUserStatusId = (int)type;
            _uow.GetRepository<AdvertisementAppUser>().Update(changed, unchanged);
            await _uow.SaveChangeAsync();
        }


    }
}
