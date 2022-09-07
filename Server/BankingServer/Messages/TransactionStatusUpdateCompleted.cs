using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TransactionStatusUpdateCompleted : IEvent
    {
        public Guid TransactionId { get; set; }
        public bool IsTransactionStatudUpdated { get; set; }
    }
}
