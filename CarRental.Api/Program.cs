using CarRental.Api.ApiAutoMapper;
using CarRental.BLL.AutoMapper;
using System.Text.Json.Serialization;
using CarRental.BLL.DependencyInjections;
using CarRental.DAL.DependencyInjections;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CarRental.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAutoMapper(typeof(AutomapperProfile));
        builder.Services.AddAutoMapper(typeof(AutomapperProfileBLL));

        builder.Services.AddDALRepositories(builder.Configuration);
        builder.Services.AddBLLServices();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.AllowTrailingCommas = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Car Rental API",
                Description = "API for managing Car, Customers, Booking and related information in a Car Rental Store.",
                Contact = new OpenApiContact
                {
                    Name = "Otar Iluridze",
                    Email = "otar_iluridze@epam.com",
                    Url = new Uri("https://api.CarRentalStore.com"),
                },
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API v1");
            });

            app.UseDeveloperExceptionPage();

        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
