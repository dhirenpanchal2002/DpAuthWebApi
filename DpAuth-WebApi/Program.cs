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
using Azure.Storage.Blobs;
using MongoDB.Driver.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Azure.Identity;
using MassTransit.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using OpenApiSecurityScheme = Microsoft.OpenApi.Models.OpenApiSecurityScheme;
using Swashbuckle.AspNetCore.Filters;

namespace DpAuthWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. [ConfigureServices]
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
               (options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {   
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),

                           ValidateIssuer = false,
                           ValidateAudience = false,
                           ValidateIssuerSigningKey = true,
                           ValidateLifetime = true,
                       };
                   }
               );

            builder.Services.AddAuthorization();


            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoDBSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            builder.Services.AddScoped(typeof(IMongoRepository<UserDocument>), typeof(MongoRepository<UserDocument>));
            builder.Services.AddScoped<IMongoRepository<TeamDocument>, MongoRepository<TeamDocument>>();
            builder.Services.AddScoped<IMongoRepository<LeaveDocument>, MongoRepository<LeaveDocument>>();
            builder.Services.AddScoped<IMongoRepository<TodoDocument>, MongoRepository<TodoDocument>>();
            //builder.Services.AddScoped<IMongoRepository<UserDocument>, MongoRepository<UserDocument>>();
            
            // Inside of Program.cs
            builder.Services.AddSingleton<BlobServiceClient>(x =>
                new BlobServiceClient(
                    "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1"
                    ));
            /*
            builder.Services.AddAzureClients(clientBuilder =>
            {
                // Register clients for each service
                //clientBuilder.AddSecretClient(new Uri("<key_vault_url>"));
                clientBuilder.AddBlobServiceClient(new Uri("http://127.0.0.1:10000/devstoreaccount1"), new DefaultAzureCredential());
                //clientBuilder.AddServiceBusClientWithNamespace("<your_namespace>.servicebus.windows.net");
                clientBuilder.UseCredential(new DefaultAzureCredential());

                // Register a subclient for each Service Bus Queue
                foreach (string queue in queueNames)
                {
                    clientBuilder.AddClient<ServiceBusSender, ServiceBusClientOptions>(
                        (_, _, provider) => provider.GetService<ServiceBusClient>()
                                .CreateSender(queue)).WithName(queue);
                }
            });
            */


            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ILeaveService, LeaveService>();
            builder.Services.AddScoped<ITodoService, TodoService>();


            //Add Masstransit RabbitMQ
            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, config) =>
                {

                    config.Host("localhost", h =>
                    {
                        h.Username("admin");
                        h.Password("admin@123");
                    });

                    config.ConfigureEndpoints(context);

                });
            });

       

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
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "DP API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                Microsoft.AspNetCore.Builder.SwaggerBuilderExtensions.UseSwagger(app);
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();                        

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}