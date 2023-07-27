using DPAuth.Messages.Abstractions.Enums;

namespace DPAuth.Messages.Abstractions.AuthEvents
{
    public interface IUserLoggedInEvent : IAuthEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public LoginDevice LoggedInDevice { get; set; }
    }

}
