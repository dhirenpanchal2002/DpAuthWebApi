using DpAuthWebApi.Enums;

namespace DpAuthWebApi.Contracts
{
    public class TodoDetail
    {
        public string Id { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }
                
        public string Status { get; set; } = TodoStatus.Pending;
        
        public DateTimeOffset? CompletedOn { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
    }
}
