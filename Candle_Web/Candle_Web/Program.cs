using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Mapper;
using Service.Services;
using Service.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Mapper
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//Add Repo
builder.Services.AddScoped<ICandleRepo, CandleRepo>();

//Add Service
builder.Services.AddScoped<ICandleService, CandleService>();
builder.Services.AddDbContext<CandleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Candle"));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
