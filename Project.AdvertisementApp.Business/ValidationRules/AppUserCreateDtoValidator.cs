using FluentValidation;
using Project.AdvertisementApp.Dtos;

namespace Project.AdvertisementApp.Business.ValidationRules
{
    public class AppUserCreateDtoValidator :AbstractValidator<AppUserCreateDto>
    {
        public AppUserCreateDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.SurName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
        }
    }
}
