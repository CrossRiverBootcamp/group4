using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Account.DTO
{
    public class CashboxDTO
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public DateTime Duration { get; set; }
        [Required]
        public int Percentages { get; set; }

    }
}
