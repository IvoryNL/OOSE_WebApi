using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebAPI;
using WebAPI.Entities;
using WebAPI.Handlers;
using WebAPI.Handlers.Interfaces;
using WebAPI.Mappers;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "OOSE_API", Version = "v1" });
    options.EnableAnnotations();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Repositories
builder.Services.AddScoped<IRepository<Auteur>, AuteurRepository>();
builder.Services.AddScoped<IRepository<Beoordeling>, BeoordelingRepository>();
builder.Services.AddScoped<IRepository<Beoordelingscriteria>, BeoordelingscriteriaRepository>();
builder.Services.AddScoped<IRepository<Beoordelingsdimensie>, BeoordelingsdimensieRepository>();
builder.Services.AddScoped<IRepository<Beoordelingsmodel>, BeoordelingsmodelRepository>();
builder.Services.AddScoped<IRepository<Beoordelingsonderdeel>, BeoordelingsonderdeelRepository>();
builder.Services.AddScoped<IGebruikerRepository<Gebruiker>, GebruikerRepository>();
builder.Services.AddScoped<IRepository<Klas>, KlasRepository>();
builder.Services.AddScoped<IRepository<Leerdoel>, LeerdoelRepository>();
builder.Services.AddScoped<IRepository<Leeruitkomst>, LeeruitkomstRepository>();
builder.Services.AddScoped<IRepository<Les>, LesRepository>();
builder.Services.AddScoped<IRepository<Lesmateriaal>, LesmateriaalRepository>();
builder.Services.AddScoped<IRepository<LesmateriaalInhoud>, LesmateriaalInhoudRepository>();
builder.Services.AddScoped<IRepository<LesmateriaalType>, LesmateriaalTypeRepository>();
builder.Services.AddScoped<IRepository<LesmateriaalVorm>, LesmateriaalVormRepository>();
builder.Services.AddScoped<IRepository<Onderwijseenheid>, OnderwijseenheidRepository>();
builder.Services.AddScoped<IOnderwijsmoduleRepository<Onderwijsmodule>, OnderwijsmoduleRepository>();
builder.Services.AddScoped<IRepository<Onderwijsuitvoering>, OnderwijsuitvoeringRepository>();
builder.Services.AddScoped<IRepository<Opleiding>, OpleidingRepository>();
builder.Services.AddScoped<IRepository<Opleidingsprofiel>, OpleidingsprofielRepository>();
builder.Services.AddScoped<IRepository<Planning>, PlanningRepository>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRepository<Status>, StatusRepository>();
builder.Services.AddScoped<IRepository<Tentamen>, TentamenRepository>();
builder.Services.AddScoped<IRepository<TentamenVanStudent>, TentamensVanStudentRepository>();
builder.Services.AddScoped<IRepository<Toetsinschrijving>, ToetsinschrijvingRepository>();
builder.Services.AddScoped<IRepository<Vorm>, VormRepository>();

// Mappers
builder.Services.AddScoped<IEntityMapper<Gebruiker, GebruikerModelDto>, GebruikerModelMapper>();
builder.Services.AddScoped<IDtoMapper<Gebruiker, IngelogdeGebruikerModelDto>, IngelogdeGebruikerMapper>();
builder.Services.AddScoped<IDtoMapper<Gebruiker, GebruikerModelMetRolDto>, GebruikerModelMetRolMapper>();

// Handlers
builder.Services.AddScoped<IHandlerAsync<bool>, ConsistentieCheckHandlerAsync>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();