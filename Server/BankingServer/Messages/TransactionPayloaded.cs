using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TransactionPayloaded:IEvent
    {
        public Guid TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Amount { get; set; }
    }
}
