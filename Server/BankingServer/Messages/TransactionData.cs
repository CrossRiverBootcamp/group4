using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TransactionData : ContainSagaData
    {
        public Guid TransactionId { get; set; }
        public bool IsTransactionFinished { get; set; }
        public bool IsTransactionStatusUpdateCompleted { get; set; }
    }
}
