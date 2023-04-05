using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.UI.Extensions;
using Project.AdvertisementApp.UI.Models;
using System.Diagnostics;

namespace Project.AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedServiceService _providedServiceService;
        private readonly IAdvertisementService _advertisementService;

        public HomeController(IProvidedServiceService providedServiceService, IAdvertisementService advertisementService)
        {
            _providedServiceService = providedServiceService;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
           var response =  await _providedServiceService.GetAllAsync();
            return this.ResponseView(response);
        }
        public async Task<IActionResult> HumanResource()
        {
            var response = await _advertisementService.GetActivesAsync();
            return this.ResponseView(response);
        }

        
    }
}