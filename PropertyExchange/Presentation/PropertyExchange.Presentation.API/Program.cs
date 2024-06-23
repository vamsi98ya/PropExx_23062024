using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PropertyExchange.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://localhost:44311/").
                    UseStartup<Startup>();
                });
    }
}



//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using PropertyExchange.Infrastructure.Data.Common;
//using PropertyExchange.Presentation.API.Automapper;
//using PropertyExchange.Presentation.API.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();

//var appSettingsSection = builder.Configuration.GetSection("AppSettings");
//builder.Services.Configure<AppSettings>(appSettingsSection);
//////Jwt Authentication
//var appSettings = appSettingsSection.Get<AppSettings>();
//var key = Encoding.ASCII.GetBytes(appSettings.Key);

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});


//builder.Services.AddDbContext<PEDbContext>(options =>
//{
//    options.UseMySql(builder.Configuration.GetConnectionString("PEDbContextConnectionString"), new MySqlServerVersion(new Version(8, 0, 35)));
//});

//builder.Services.AddScoped<PEDbContext>();
//builder.Services.AddAutoMapper(typeof(AutomapperAPIProfile));
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllersWithViews();
//builder.Services.AddCors();



//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.UseSwagger();
//    //app.UseSwaggerUI();
//}




//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseCors(builder =>
//               builder
//                   .AllowAnyOrigin()
//                   .AllowAnyHeader()
//                   .AllowAnyMethod()
//                   );

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
