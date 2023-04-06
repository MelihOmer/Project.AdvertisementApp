using Project.AdvertisementApp.Dtos.Interfaces;

namespace Project.AdvertisementApp.Dtos
{
    public class AppRoleUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }

    }
}
