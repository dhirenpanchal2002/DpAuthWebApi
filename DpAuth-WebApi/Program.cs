using DpAuthWebApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using MongoDB.Driver;
using DpAuthWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DpAuthWebApi.Models;
using MassTransit;

namespace DpAuthWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. [ConfigureServices]

            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoDBSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            builder.Services.AddScoped(typeof(IMongoRepository<UserDocument>), typeof(MongoRepository<UserDocument>));
            builder.Services.AddScoped<IMongoRepository<TeamDocument>, MongoRepository<TeamDocument>>();
            builder.Services.AddScoped<IMongoRepository<LeaveDocument>, MongoRepository<LeaveDocument>>();
            //builder.Services.AddScoped<IMongoRepository<UserDocument>, MongoRepository<UserDocument>>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ILeaveService, LeaveService>();
            

            ////Add Masstransit RabbitMQ
            //builder.Services.AddMassTransit(x =>
            //{
            //    x.SetKebabCaseEndpointNameFormatter();

            //    x.UsingRabbitMq((context, config) => {

            //            config.Host("localhost", h =>
            //            {
            //                h.Username("admin");
            //                h.Password("admin@123");
            //            });

            //            config.ConfigureEndpoints(context);

            //    });
            //});

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                (options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
                );

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000",
                                            "https://localhost:3001")
                                .AllowAnyHeader()
                                .AllowAnyMethod(); 
                    });
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                Microsoft.AspNetCore.Builder.SwaggerBuilderExtensions.UseSwagger(app);
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}