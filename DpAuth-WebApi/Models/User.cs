using DpAuthWebApi.Repository.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DpAuthWebApi.Repository;

namespace DpAuthWebApi.Models
{
    [BsonCollection("User")]
    public class UserDocument: Document
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string PhotoUrl { get; set; }
        
        //byte[]
        public string PwdHash { get; set; }

        //byte[]
        public string PwdSalt { get; set; }

        public Boolean IsDeleted { get; set; } = false;
    }
}
