using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.DAL.Entities
{
    public class TransactionEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int FromAccountId { get; set; }
        [Required]
        public int ToAccountId { get; set; }
        [Required]
        [Range(1,100000000)]
        public int Amount { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }
        [Required]
        public TransactionStatus Status { get; set; }
        public string FailureReason { get; set; }


    }
}
