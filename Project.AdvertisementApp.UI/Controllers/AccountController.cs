using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateVM> _validator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateVM> validator, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _validator = validator;
            _appUserService = appUserService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateVM
            {
                Genders = new SelectList(response.Data, "Id", "Definition")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateVM model)
        {
            var result = _validator.Validate(model);
            if (result.IsValid)
            {
                var dto = _mapper.Map<AppUserCreateDto>(model);
                var createResponse = await _appUserService.CreateWithRoleAsync(dto, (int)RoleType.Member);
                return this.ResponseRedirectAction(createResponse, "SignIn");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Definition", model.GenderId);
            return View(model);

        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {
            var result = await _appUserService.CheckUserAsync(dto);
            if (result.ResponseType == Common.ResponseType.Success)
            {
                var claims = new List<Claim>();


                var roleResult =  await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
               if (roleResult.ResponseType == Common.ResponseType.Success)
                {
                    foreach (var role in roleResult.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,role.Definition));
                    }
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,result.Data.Id.ToString()));
                }
                


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe
                };

                await HttpContext.SignInAsync
                    (
                    CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("Kullanıcı Adı Veya Parola Geçersiz.", result.Message);
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}
