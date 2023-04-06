using AutoMapper;
using FluentValidation;
using Project.AdvertisementApp.Business.Extensions;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Common;
using Project.AdvertisementApp.DataAccess.UnitOfWork;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.Services
{
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IValidator<AppUserCreateDto> _createValidator;
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserLoginDto> _LoginValidator;
        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow, IValidator<AppUserLoginDto> loginValidator)
            : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _createValidator = createDtoValidator;
            _uow = uow;
            _mapper = mapper;
            _LoginValidator = loginValidator;
        }
        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto,int roleId)
        {
            var validationResult =_createValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user = _mapper.Map<AppUser>(dto);
               await _uow.GetRepository<AppUser>().CreateAsync(user);

               await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                {
                    AppRoleId = roleId,
                    AppUser = user

                });
               await _uow.SaveChangeAsync();
                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
                //
            }
            return new Response<AppUserCreateDto>(dto, validationResult.ConvertCustomValidationError());

        }
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
        {
            var validationResult = _LoginValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user =await _uow.GetRepository<AppUser>().GetByFilterAsync(x => x.UserName == dto.UserName && x.Password == dto.Password);
                if (user != null)
                {
                    var appUserDto = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success, appUserDto);
                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı Adı Veya Şifre Hatalı");
            }
            return new Response<AppUserListDto>(ResponseType.ValidationError, "Kullanıcı Adı Veya Şifre Boş Olamaz");
        }
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {
          var roles =   await _uow.GetRepository<AppRole>().GetAllAsync(x => x.AppUserRoles.Any( x =>x.AppUserId == userId));
            if (roles==null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "İlgili Rol Bulunamadı");
            }
            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        }
    }
}
