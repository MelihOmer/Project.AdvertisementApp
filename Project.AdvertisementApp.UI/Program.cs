using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project.AdvertisementApp.Business.DependencyResolvers.Microsoft;
using Project.AdvertisementApp.Business.Helpers;
using Project.AdvertisementApp.UI.Mapping.AutoMapper;
using Project.AdvertisementApp.UI.Models;
using Project.AdvertisementApp.UI.ValidationRulesVM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddTransient<IValidator<UserCreateVM>, UserCreateVMValidator>();

var profiles = ProfileHelper.GetProfiles();
profiles.Add(new UserCreateVMProfile());

var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});
var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "ProjectPoseidon";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.ExpireTimeSpan = TimeSpan.FromDays(20);
    opt.LoginPath = new PathString("/Account/SignIn");
    opt.LogoutPath = new PathString("/Account/LogOut");
    opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
