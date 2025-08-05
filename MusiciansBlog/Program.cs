using Mappify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using MusiciansBlog.API.Authentication.Extensions;
using MusiciansBlog.API.Authentication.Hashers;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Infrastructure;
using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using MusiciansBlog.API.Infrastructure.Users.Common;
using MusiciansBlog.API.Middleware;
using Serilog;
using StackExchange.Redis;

namespace MusiciansBlog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MyDbContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("PostgresConnection")
                    )
                );

            //logger
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(logger);

            //redis
            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
                
            });

            //options
            builder.Services.Configure<RedisCacheUsersOptions>(
                builder.Configuration.GetSection(nameof(RedisCacheUsersOptions)));

            //cache
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!));

            builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
            builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<IRedisProvider, RedisProvider>();
            builder.Services.AddScoped<ICookieProvider, CookieProvider>();  
            builder.Services.AddScoped<IJWTProvider,  JWTProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //mapper
            builder.Services.AddMappify();
            builder.Services.AddMappifyProfileForAssembly(typeof(Program));

            //authentication
            builder.Services.AddJwtSupport(builder.Configuration);


            var app = builder.Build();

            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
