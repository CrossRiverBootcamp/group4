using NServiceBus;

namespace Messages
{
    public class TransactionPayloaded :IEvent
    {
        public Guid TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Amount { get; set; }
    }
}