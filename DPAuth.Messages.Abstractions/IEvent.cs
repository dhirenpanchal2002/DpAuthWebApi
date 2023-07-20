using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPAuth.Messages.Abstractions
{
    public interface IEvent
    {
        public Guid MessageId => System.Guid.NewGuid();
        public DateTimeOffset CreatedAt => DateTimeOffset.Now;
    }
}
