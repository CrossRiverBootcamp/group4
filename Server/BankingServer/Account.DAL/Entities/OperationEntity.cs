using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.Entities
{
    public class OperationEntity
    {
        [Key]
        [Required]
        public int Id{ get; set; }
        [Required]
        public int AccountId{ get; set; }
        [Required]
        public string TransactionId{ get; set; }
        [Required]
        public bool DebitOrCredit { get; set; }
        [Required]
        public int TransactionAmount { get; set; }
        [Required]
        public int Balance { get; set; }
        [Required]
        public DateTime OperationTime { get; set; }

    }
}
