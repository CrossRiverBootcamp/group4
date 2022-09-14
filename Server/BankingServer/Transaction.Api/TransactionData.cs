using NServiceBus;

namespace Transaction.Api
{
    public class TransactionData : ContainSagaData
    {
        public Guid TransactionId { get; set; }
        public bool GetEventPayload { get; set; }
        public bool IsBalanceUpdated { get; set; }
    }
}
