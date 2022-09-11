using NServiceBus;

namespace Messages
{
    public class TransactionPayload :ICommand
    {
        public Guid TransactionId { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public int Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}