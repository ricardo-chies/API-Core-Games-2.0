using AutoMapper;
using GamesAPI.Context;
using GamesAPI.DTO.Mapping;
using GamesAPI.Repository;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
