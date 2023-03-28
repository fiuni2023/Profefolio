using Microsoft.EntityFrameworkCore;
using profefolio.Models;
using AutoMapper;
using profefolio.Models.Entities;
using profefolio.Repository;
using profefolio.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using log4net;
using log4net.Config;
using System.IO;
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Connection Strings
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Context"))
);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios
builder.Services.AddScoped<IPersona, PersonasService>();
builder.Services.AddScoped<IColegio, ColegiosService>();
builder.Services.AddScoped<IMateria, MateriaService>();
builder.Services.AddScoped<ICiclo, CicloService>();
builder.Services.AddScoped<IClase, ClaseService>();
builder.Services.AddScoped<IFullColegio, ColegiosFullService>();
builder.Services.AddScoped<IRol, RolService>();
builder.Services.AddScoped<IAuth, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();