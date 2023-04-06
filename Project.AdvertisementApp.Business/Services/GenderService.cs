using AutoMapper;
using FluentValidation;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.DataAccess.UnitOfWork;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.Services
{
    public class GenderService : Service<GenderCreateDto, GenderUpdateDto, GenderListDto, Gender>, IGenderService
    {
        public GenderService(IMapper mapper, IValidator<GenderCreateDto> createDtoValidator, IValidator<GenderUpdateDto> updateDtoValidator, IUow uow) 
            : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
        }
    }
}
