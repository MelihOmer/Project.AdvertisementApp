using FluentValidation;
using Project.AdvertisementApp.Common.Enums;
using Project.AdvertisementApp.Dtos;

namespace Project.AdvertisementApp.Business.ValidationRules
{
    public class AdvertisementAppUserCreateDtoValidator :AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertisementAppUserCreateDtoValidator()
        {
            RuleFor(x => x.AdvertisementAppUserStatusId).NotEmpty();
            RuleFor(x => x.AdvertisementId).NotEmpty();
            RuleFor(x => x.AppUserId).NotEmpty();
            RuleFor(x => x.CvPath).NotEmpty().WithMessage("Cv Dosyası Zorunludur.");
            RuleFor(x => x.EndDate).NotEmpty().When(x => x.MilitaryStatusId ==(int)MilitaryStatusType.Tecilli).WithMessage("Tecil Bitiş Tarihi Boş Olamaz.");
        }

    }
}
