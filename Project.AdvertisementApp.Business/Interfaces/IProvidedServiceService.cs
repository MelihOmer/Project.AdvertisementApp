using Project.AdvertisementApp.Common;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.Interfaces
{
    public interface IProvidedServiceService:
        IService<ProvidedServiceCreateDto,ProvidedServiceUpdateDto,ProvidedServiceListDto,ProvidedService>
    {
        
    }
}
