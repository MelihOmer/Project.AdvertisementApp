using Project.AdvertisementApp.DataAccess.Contexts;
using Project.AdvertisementApp.DataAccess.Interfaces;
using Project.AdvertisementApp.DataAccess.Repositories;
using Project.AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly AdvertisementContext _context;

        public Uow(AdvertisementContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T:BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangeAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
