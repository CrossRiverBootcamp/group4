using System.ComponentModel.DataAnnotations;

namespace Account.DTO
{
    public class OperationMapDTO
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public bool DebitOrCredit { get; set; }

        [Required]
        public int TransactionAmount { get; set; }

        [Required]
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
