using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusiciansBlog.API.Authentication.Options;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MusiciansBlog.API.Authentication.Extensions
{
    /// <summary>
    /// Класс с методом расширения для конфигурации JWT для Program.cs
    /// </summary>
    public static class AuthExtension
    {
        public static void AddJwtSupport(this IServiceCollection collection, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(JwtOptions));
            var googleConfig = configuration.GetSection("Authentication:Google"); 

            collection.Configure<JwtOptions>(config);

            var jwtOptions = config.Get<JwtOptions>()!;

            collection.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer, //издатель токена
                    ValidAudience = jwtOptions.Audience, //потребитель токена
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)) //ключ для проверки подписи токена
                };

                opt.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access"];

                        return Task.CompletedTask;
                    }
                };
            })
            .AddGoogleOpenIdConnect(opt => //google support
                {
                    opt.ClientId = googleConfig["ClientId"];
                    opt.ClientSecret = googleConfig["ClientSecret"];
                });
        }
    }
}
