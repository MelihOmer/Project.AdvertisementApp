using Project.AdvertisementApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Dtos
{
    public class GenderListDto :IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
