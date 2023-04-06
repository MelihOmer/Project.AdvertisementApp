using FluentValidation;
using Project.AdvertisementApp.UI.Models;

namespace Project.AdvertisementApp.UI.ValidationRulesVM
{
    public class UserCreateVMValidator :AbstractValidator<UserCreateVM>
    {
        //[Obsolete]
        public UserCreateVMValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola Boş Olamaz.");
            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Parola En Az 3 Karakter Olmalıdır.");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Parolalar Eşleşmiyor.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad Alanı Boş Olamaz.");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("SoyAd Alanı Boş Olamaz.");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Kullanıcı En Az 3 Karakter Olmalıdır.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz.");
            RuleFor(x => new
            {
                x.UserName,
                x.FirstName
            }).Must(x => CanNotFirstName(x.UserName,x.FirstName)).WithMessage("Kullanıcı Adı - Adınız İle Aynı Olamaz.").When(x=>x.UserName != null && x.FirstName!= null);
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Cinsiyet Alanı Zorunludur.");
        }

        private bool CanNotFirstName(string userName,string firstName)
        {
            return !userName.Equals(firstName);
        }
    }
}
