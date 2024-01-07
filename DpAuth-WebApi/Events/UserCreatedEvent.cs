using DPAuth.Messages.Abstractions.AuthEvents;
using DPAuth.Messages.Abstractions.Enums;

namespace DpAuthWebApi.Events
{
    public class UserCreatedEvent : IUserLoggedInEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public LoginDevice LoggedInDevice { get; set; }
    }
}
