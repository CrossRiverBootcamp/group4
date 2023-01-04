using NServiceBus;

namespace Messages
{
    public class TransactionPayloaded:IEvent
    {
        public Guid TransactionId { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public float Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}
