namespace DpAuthWebApi.Contracts
{
    public class UserDetails
    {
        public string Id { get; set; }        
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string PhotoUrl { get; set; }

        public Boolean IsDeleted { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
