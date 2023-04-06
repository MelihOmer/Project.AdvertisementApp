﻿using FluentValidation;
using Project.AdvertisementApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AdvertisementApp.Business.ValidationRules
{
    public class GenderCreateDtoValidator:AbstractValidator<GenderCreateDto>
    {
        public GenderCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
        }
    }
}
