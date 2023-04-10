using Project.AdvertisementApp.Dtos.Interfaces;

namespace Project.AdvertisementApp.Dtos
{
    public class MilitaryStatusListDto :IDto
    {
        public int Id { get; set; }
        public string Definiton { get; set; }
    }
}
