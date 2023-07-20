using DpAuthWebApi.Enums;

namespace DpAuthWebApi.Contracts
{
    public class LeaveDetail
    {
        public string Id { get; set; }
        
        public string UserId { get; set; }

        public string Description { get; set; }

        public LeaveCategory leaveDay { get; set; } = LeaveCategory.FullDay;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public LeaveStatus Status { get; set; } = LeaveStatus.PendingApproval;

        public LeaveType Type { get; set; } = LeaveType.AnnualLeave;

        public UserDetails Approver { get; set; }

        public DateTimeOffset ApprovedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
