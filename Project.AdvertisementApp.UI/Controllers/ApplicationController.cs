using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.UI.Extensions;

namespace Project.AdvertisementApp.UI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ApplicationController : Controller
    {
        private readonly IAdvertisementService _advertisementService;

        public ApplicationController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> List()
        {
            var response =await _advertisementService.GetAllAsync();
            return this.ResponseView(response);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AdvertisementCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdvertisementCreateDto dto)
        {

            var response = await _advertisementService.CreateAsync(dto);

            return this.ResponseRedirectAction(response, "List");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _advertisementService.GetByIdAsync<AdvertisementUpdateDto>(id);
            return this.ResponseView(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementUpdateDto dto)
        {
            var response = await _advertisementService.UpdateAsync(dto);
            return this.ResponseRedirectAction(response,"List");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response = await _advertisementService.Remove(id);
            return this.ResponseRedirectAction(response, "List");
        }
    }
}
