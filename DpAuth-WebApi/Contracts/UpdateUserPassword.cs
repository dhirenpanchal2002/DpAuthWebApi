namespace DpAuthWebApi.Contracts
{
    public class UpdateUserPassword
    {
        public string EmailId { get; set; }
        public string VerificationCode { get; set; }
        public string Password { get; set; }
    }
}