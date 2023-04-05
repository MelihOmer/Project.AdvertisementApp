using FluentValidation.Results;
using Project.AdvertisementApp.Common;

namespace Project.AdvertisementApp.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationError> ConvertCustomValidationError(this ValidationResult validationResult )
        {
            List<CustomValidationError> errors = new();
            foreach (var item in validationResult.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName,
                });
            }
            return errors;
        }
    }
}
