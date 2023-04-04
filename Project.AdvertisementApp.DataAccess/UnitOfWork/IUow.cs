using Project.AdvertisementApp.DataAccess.Interfaces;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.DataAccess.UnitOfWork
{
    public interface IUow
    {

        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChangeAsync();
    }
}
