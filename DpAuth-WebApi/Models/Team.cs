using DpAuthWebApi.Repository.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DpAuthWebApi.Repository;

namespace DpAuthWebApi.Models
{
    [BsonCollection("Team")]
    public class TeamDocument : Document
    {
        public string TeamName { get; set; }

        public string LeadUserId { get; set; }
        
        public string TeamEmailId { get; set; }

        public Boolean IsDeleted { get; set; } = false;

        public List<string> MemberIds { get; set; }
    }
}
