using Project.AdvertisementApp.Dtos.Interfaces;

namespace Project.AdvertisementApp.Dtos
{
    public class AppRoleListDto :IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
