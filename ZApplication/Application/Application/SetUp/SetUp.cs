using Application.Helper;
using Application.Interface;
using Application.Service.ResponseService;
using Application.Service.UserService;
using Application.SetUp.Model;
using Domain.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.SetUp
{
    public static class SetUp
    {
        public static void AddAllApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddSingleton(typeof(Mapper<>));
          
        }

        public static void AddApplicationDBContext(this IServiceCollection services ,string connectionString)
        {
            services.AddDbContext<ApplicationDBContext> (options =>
            {
                options.UseSqlServer(connectionString);
            });
        
        }
        public static void AddJWTConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JWTConfig"));

        }       
        public static IServiceCollection AddJWT(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            JwtConfig configs = sp.GetService<IOptions<JwtConfig>>().Value;
            var key = Encoding.UTF8.GetBytes(configs.TokenKey);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(configs.TokenTimeOut),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }


    }
}
