using profefolio.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using profefolio.Models.Entities;
using profefolio.Repository;
using profefolio.Services;
using log4net;
using log4net.Config;
using profefolio;
using System.Reflection;
using Microsoft.OpenApi.Models;

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

builder.Services.AddCors(p => p.AddPolicy("corsapp", b =>
{
    b.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Identity
builder.Services.AddIdentity<Persona, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });


// Configuracion de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Profefolio"
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});



//Servicios
builder.Services.AddScoped<IPersona, PersonasService>();
builder.Services.AddScoped<IProfesor, ProfesorService>();
builder.Services.AddScoped<IColegio, ColegiosService>();
builder.Services.AddScoped<IColegioProfesor, ColegioProfesorService>();
builder.Services.AddScoped<IColegiosAlumnos, ColegiosAlumnosServices>();
builder.Services.AddScoped<IDashboardProfesor, DashboardProfesorService>();
builder.Services.AddScoped<IMateria, MateriaService>();
builder.Services.AddScoped<ICiclo, CicloService>();
builder.Services.AddScoped<IClase, ClaseService>();
builder.Services.AddScoped<IClasesAlumnosColegio, ClasesAlumnosColegioService>();
builder.Services.AddScoped<IFullColegio, ColegiosFullService>();
builder.Services.AddScoped<IHoraCatedra, HoraCatedraService>();
builder.Services.AddScoped<IHorasCatedrasMaterias, HorasCatedrasMateriasService>();
builder.Services.AddScoped<IRol, RolService>();
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IMateriaLista, MateriaListaService>();
builder.Services.AddScoped<IAdmin, AdminReportService>();
builder.Services.AddScoped<IEvento, EventoService>();
builder.Services.AddScoped<IAsistencia, AsistenciaService>();
builder.Services.AddScoped<IAnotacion, AnotacionesService>();
builder.Services.AddScoped<IDocumento, DocumentoService>();
builder.Services.AddScoped<IContactoEmergencia, ContactoEmergenciaService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();