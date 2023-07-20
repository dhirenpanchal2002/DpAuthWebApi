using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAuth.Messages.Abstractions.AuthEvents
{
    public interface IPasswordChangedEvent: IAuthEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
    }
}
