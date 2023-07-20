namespace DpAuthWebApi.Contracts
{
    public class TeamDetails
    {
        public string Id { get; set; }
        public string TeamName { get; set; }

        public UserDetails TeamLead { get; set; }

        public string TeamEmailId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public List<UserDetails> TeamMembers { get; set; }
    }
}
