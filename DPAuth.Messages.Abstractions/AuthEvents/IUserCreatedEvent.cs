using DPAuth.Messages.Abstractions.AuthEvents;
using DPAuth.Messages.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DpAuth.Messages.Abstractions.AuthEvents
{
    internal interface IUserCreatedEvent : IAuthEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public LoginDevice LoggedInDevice { get; set; }
    }
}
