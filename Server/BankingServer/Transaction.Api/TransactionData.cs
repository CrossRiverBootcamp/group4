using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Api
{
    public class TransactionData : ContainSagaData
    {
        public Guid TransactionId { get; set; }
        public bool GetEventPayload { get; set; }

        public bool IsBalanceUpdated { get; set; }
    }
}
