using Api.Bootstrapping.CustomExceptions;
using Api.Bootstrapping.Extensions;
using Api.Bootstrapping.Middleware;
using CarRental.Api.ApiAutoMapper;
using CarRental.BLL.AutoMapper;
using CarRental.BLL.DependencyInjections;
using CarRental.BLL.Models.Settings;
using CarRental.BLL.Services;
using CarRental.DAL.DependencyInjections;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace CarRental.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureLogger();
        builder.Host.UseSerilog();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        builder.Services.ConfigureExceptionHandlingMiddleware(new Dictionary<Type, HttpStatusCode>
        {
            [typeof(BadRequestException)] = HttpStatusCode.BadRequest,
            [typeof(ValidationException)] = HttpStatusCode.BadRequest,
            [typeof(NotFoundException)] = HttpStatusCode.NotFound,
            [typeof(ForbiddenException)] = HttpStatusCode.Forbidden,
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAutoMapper(typeof(AutomapperProfile));
        builder.Services.AddAutoMapper(typeof(AutomapperProfileBLL));

        var authApiBaseUrl = builder.Configuration["ApiSettings:AuthApiUrl"];

        var authApiHttpClient = builder.Services.AddHttpClient("GameStore.Auth.Api", client =>
        {
            client.BaseAddress = new Uri(authApiBaseUrl);
        });

        var authApiSettings = new AuthApiSettings
        {
            AuthApiUrl = authApiBaseUrl,
        };


        builder.Services.AddDALRepositories(builder.Configuration);
        builder.Services.AddBLLServices(authApiSettings);

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

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "[ANALYTICS] API endpoint call info";
            options.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
            {
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("ResponseStatusCode", httpContext.Response.StatusCode);
            };
        });


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API v1");
            });

            app.UseDeveloperExceptionPage();

        }
        else
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var exceptionHandler = context
                        .RequestServices
                        .GetRequiredService<GlobalExceptionHandler>();

                    await exceptionHandler
                        .TryHandleAsync(
                        context,
                        context.Features.Get<IExceptionHandlerFeature>().Error,
                        CancellationToken.None);
                },
            });
        }


        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.UseHangfireDashboard();

        var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
        recurringJobManager.AddOrUpdate<ReservationScheduler>(
            "update-reservations",
            scheduler => scheduler.UpdateReservationsAsync(),
            Cron.Hourly);

        app.Run();

    }
}
