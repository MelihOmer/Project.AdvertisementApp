using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.AdvertisementApp.Business.Interfaces;
using Project.AdvertisementApp.Business.Mappings.AutoMapper;
using Project.AdvertisementApp.Business.Services;
using Project.AdvertisementApp.Business.ValidationRules;
using Project.AdvertisementApp.DataAccess.Contexts;
using Project.AdvertisementApp.DataAccess.UnitOfWork;
using Project.AdvertisementApp.Dtos;

namespace Project.AdvertisementApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdvertisementContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("local")));


            

            
            //Validator Class

            services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
            services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();

            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();

            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();

            services.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();
            services.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();
            

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IProvidedServiceService,ProvidedServiceService>();
            services.AddScoped<IAdvertisementService,AdvertisementService>();
            services.AddScoped<IAppUserService,AppUserService>();
            services.AddScoped<IGenderService,GenderService>();

        }
        
    }
}
