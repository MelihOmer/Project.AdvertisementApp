﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Common.Enums;
using Project.AdvertisementApp.Dtos;
using Project.AdvertisementApp.UI.Extensions;
using Project.AdvertisementApp.UI.Models;
using System.Security.Claims;

namespace Project.AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService userService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _userService = userService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
           var userId =  int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);
            var items = Enum.GetValues(typeof(MilitaryStatusType));
            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto
                {
                    Id = item,
                    Definiton = Enum.GetName(typeof(MilitaryStatusType), item)
                });
            }
            ViewBag.MilitaryStatusList = new SelectList(list, "Id", "Definition");

            ViewBag.GenderId = userResponse.Data.GenderId;
            return View(new AdvertisementAppUserCreateModel
            {
                AdvertisementId = advertisementId,
                AppUserId = userId,
                
            });
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            AdvertisementAppUserCreateDto dto = new();
            if (model.CvFile != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.CvFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","cvFiles",fileName + extName);
                var strean = new FileStream(path, FileMode.Create);
                await model.CvFile.CopyToAsync(strean);
                dto.CvPath = path;
            }

            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.AdvertisementId = model.AdvertisementId;
            dto.AppUserId = model.AppUserId;
            dto.EndDate = model.EndDate;
            dto.MilitaryStatusId = model.MilitaryStatusId;
            dto.WorkExperience= model.WorkExperience;

            var response = await _advertisementAppUserService.CreateAsync(dto);
            if (response.ResponseType == Common.ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);
                ViewBag.GenderId = userResponse.Data.GenderId;
                var items = Enum.GetValues(typeof(MilitaryStatusType));
                var list = new List<MilitaryStatusListDto>();
                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto
                    {
                        Id = item,
                        Definiton = Enum.GetName(typeof(MilitaryStatusType), item)
                    });
                }
                ViewBag.MilitaryStatusList = new SelectList(list, "Id", "Definition");
                return View(model);
            }
            else
            {
                return this.ResponseRedirectAction(response, "HumanResource", "Home");
            }
            
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
           var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvurdu);
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int advertisementAppUserId,AdvertisementAppUserStatusType type)
        {
           await _advertisementAppUserService.SetStatusAsync(advertisementAppUserId, type);
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvurdu);
            return View("List",list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Mülakat);
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Olumsuz);
            return View(list);
        }
    }
}
