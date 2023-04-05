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
    public class ProvidedServiceService :
        Service<ProvidedServiceCreateDto,ProvidedServiceUpdateDto,ProvidedServiceListDto,ProvidedService>,IProvidedServiceService
    {
        public ProvidedServiceService(IMapper mapper,IValidator<ProvidedServiceCreateDto> createDtoValidator, IValidator<ProvidedServiceUpdateDto> updatetoValidator,IUow uow) 
            : base(mapper, createDtoValidator, updatetoValidator,uow)
        {
            
        }
    }
}
