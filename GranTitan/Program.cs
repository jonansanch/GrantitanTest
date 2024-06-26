using FluentValidation;
using GranTitan.BLL.Interface;
using GranTitan.BLL.Service;
using GranTitan.BLL.Validators;
using GranTitan.DAL.Interface;
using GranTitan.DAL.Persistance;
using GranTitan.DAL.Persistance.Repository;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using AutoMapper;
using GranTitan.BLL.Mapper;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// FluentValidation
//builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>(ServiceLifetime.Singleton);
builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>(ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

#region Auto Mapper Config
//Registra automapper
var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new MappingProfile()); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

var app = builder.Build();
app.UseFileServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapControllers();

var supportedCultures = new[]
{
        new CultureInfo("es-CO")
    };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-CO"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});

app.Run();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
