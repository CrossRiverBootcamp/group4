using System.ComponentModel.DataAnnotations;


namespace Transaction.DTO
{
    public class TransactionDto
    {
        [Required]
        public int FromAccountId { get; set; }

        [Required]
        public int ToAccountId { get; set; }

        [Required]
        [Range(1,100000000)]
        public int Amount { get; set; }
    }
}
