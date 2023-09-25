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
    [BsonCollection("Todo")]
    public class TodoDocument : Document
    {
        public string UserId { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }                

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        public DateTimeOffset? CompletedOn { get; set; }
    }
}
