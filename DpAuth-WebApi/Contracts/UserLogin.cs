namespace DpAuthWebApi.Contracts
{
    public class UserLogin
    {
        public string Password { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }

        public string Token { get; set; }
    }
}
