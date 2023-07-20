using DPAuth.Messages.Abstractions.AuthEvents;
using MassTransit;
using MassTransit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpAuth.Notifications.Handlers
{
    public class UserLoggedInEventHandler : IConsumer<IUserLoggedInEvent>
    {
        public UserLoggedInEventHandler()
        {
            Console.WriteLine($"UserLoggedInEventHandler constroctor called...");
        }
        public async Task Consume(ConsumeContext<IUserLoggedInEvent> context)
        {         
            Console.WriteLine($"Message Consumed.. {context.Message.UserName}");

            await Task.CompletedTask;
        }
    }
}
