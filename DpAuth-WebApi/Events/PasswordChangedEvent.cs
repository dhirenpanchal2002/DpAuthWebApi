using DPAuth.Messages.Abstractions.AuthEvents;

namespace DpAuthWebApi.Events
{
    public class PasswordChangedEvent : IPasswordChangedEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
    }
}
