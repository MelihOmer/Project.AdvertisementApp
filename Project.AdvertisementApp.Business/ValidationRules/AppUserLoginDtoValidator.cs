using FluentValidation;
using Project.AdvertisementApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.ValidationRules
{
    public class AppUserLoginDtoValidator: AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıdı Adı Boş Olamaz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola Alanı Boş Olamaz");
        }
    }
}
