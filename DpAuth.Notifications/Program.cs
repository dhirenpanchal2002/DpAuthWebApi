// See https://aka.ms/new-console-template for more information
using System.Net;
using DpAuth.Notifications.Handlers;
using DPAuth.Messages.Abstractions.AuthEvents;
using MassTransit;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var host = Host.CreateDefaultBuilder();

var hostbuilder = host.ConfigureServices(services =>
{
    // services.addcon<IConsumer<IUserLoggedInEvent>, UserLoggedInEventHandler>();

    services.AddMassTransit(x => { 
        
        x.AddConsumer<UserLoggedInEventHandler>();

        x.UsingRabbitMq((context, config) =>
        {

            config.Host("localhost", h =>
            {
                h.Username("admin");
                h.Password("admin@123");
            });

            config.ReceiveEndpoint("DpAuth-Queue", x => {
                x.ConfigureConsumer<UserLoggedInEventHandler>(context);
                });

        });
    });
});

hostbuilder.Build().Run();

Console.WriteLine("Starting the Notification Handler ...");
