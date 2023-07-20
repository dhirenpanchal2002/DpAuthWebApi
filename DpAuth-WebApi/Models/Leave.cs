using DpAuthWebApi.Repository.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Enums;

namespace DpAuthWebApi.Models
{
    [BsonCollection("Leave")]
    public class LeaveDocument : Document
    {
        public string UserId { get; set; }

        public string Description { get; set; }

        public LeaveCategory LeaveDay { get; set; } = LeaveCategory.FullDay;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
        
        public LeaveStatus Status { get; set; } = LeaveStatus.PendingApproval;

        public LeaveType Type { get; set; } = LeaveType.AnnualLeave;

        public string ApproverId { get; set; }

        public DateTimeOffset ApprovedAt { get; set; }
    }
}
