using Business.Services.Abstract;
using Business.Services.Concrete;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

#region builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>(); //bu idetitini hansi dbcontext uzerinden quracayiqsa

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

#region Repository

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

#endregion

#region Services

builder.Services.AddScoped<ICategoryService, CategoryService>(); //dependency injection istifade edib isft ede bilecem. her servis yaradanda edirik

#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion


#region app
var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();
#endregion

