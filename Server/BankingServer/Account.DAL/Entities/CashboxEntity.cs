using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Account.DAL.Entities
{
    public class CashboxEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AccountEntity")]
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }

        [Required]
        public float Amount { get; set; }
        [Required]
        public int PercentageOfRevenue { get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
