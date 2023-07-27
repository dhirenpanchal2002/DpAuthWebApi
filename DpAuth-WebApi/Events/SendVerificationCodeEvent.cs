using DPAuth.Messages.Abstractions.AuthEvents;

namespace DpAuthWebApi.Events
{
    public class SendVerificationCodeEvent : ISendVerificationCodeEvent
    {
        public string EmailId { get; set; }
        public string VerificationCode { get; set; }

    }
}
