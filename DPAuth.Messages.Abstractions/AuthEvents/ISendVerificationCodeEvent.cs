using DPAuth.Messages.Abstractions.Enums;

namespace DPAuth.Messages.Abstractions.AuthEvents
{
    public interface ISendVerificationCodeEvent : IAuthEvent
    {
        public string EmailId { get; set; }
        public string VerificationCode { get; set; }
    }

}
