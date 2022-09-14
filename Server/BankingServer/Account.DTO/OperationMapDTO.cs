namespace Account.DTO
{
    public class OperationMapDTO
    {
        public int AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public bool DebitOrCredit { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public OperationMapDTO(int accountId, Guid transactionId, bool debitOrCredit, int transactionAmount, DateTime dateOfTransaction)
        {
            AccountId = accountId;
            TransactionId = transactionId;
            DebitOrCredit = debitOrCredit;
            TransactionAmount = transactionAmount;
            DateOfTransaction = dateOfTransaction;
        }
    }
}
