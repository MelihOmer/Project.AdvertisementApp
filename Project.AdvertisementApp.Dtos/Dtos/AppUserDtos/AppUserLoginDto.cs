using Project.AdvertisementApp.Dtos.Interfaces;

namespace Project.AdvertisementApp.Dtos
{
    public class AppUserLoginDto :IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
