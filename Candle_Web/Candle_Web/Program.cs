using Candle_Web;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Mapper;
using Service.Services;
using Service.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Net.payOS;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(payOS);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add cloudiary
builder.Services.AddCloudinary();

//Add Mapper
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
builder.Services.AddCors(option =>
    option.AddPolicy("CORS", builder =>
        builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((host) => true)));

//add JWT
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


//----------------------------ADD-----SCOPE--------------------------------------------
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |


//Add Repo
builder.Services.AddScoped<ICandleRepo, CandleRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IReViewRepo, ReviewRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<ICateRepo, CateRepo>();



//Add Service
builder.Services.AddScoped<ICandleService, CandleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICateService, CateService>();




//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//                                                                                    |
//----------------------------ADD-----SCOPE--------------------------------------------

var connectionString = builder.Configuration.GetConnectionString("Candle");
Console.WriteLine($"MSSQL_Connection Program: {connectionString}");
builder.Services.AddDbContext<candleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Candle"));
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerPolicy", policy =>
        policy.RequireClaim("Role", "Manager"));
    options.AddPolicy("CustomerPolicy", policy =>
        policy.RequireClaim("Role", "Customer"));
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
