using AutoMapper;
using GamesAPI.Context;
using GamesAPI.DTO.Mapping;
using GamesAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
string mySqlconnection = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlconnection, 
                    ServerVersion.AutoDetect(mySqlconnection)));

// Adiocionando dependência de Unity Of Work
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();

//Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT
// Adiciona manipulador de autenticação,
// Define o esquema de autenticação usado (Bearer),
// Valida o emissor, a audiência e a chave,
// Usando a chave secreta valida a assinatura.
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
        ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

// Configure AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build(); //configure

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
