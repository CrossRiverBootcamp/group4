using NServiceBus;

namespace Messages
{
    public class BalanceUpdated:IEvent
    {
        public bool BalanceUpdatedSucceeded { get; set; }
        public Guid TransactionId { get; set; }
        public string? FailureReason { get; set; }
    }
}
