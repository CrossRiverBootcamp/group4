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

        public int AccountId { get; set; }
        public int Duration { get; set; }
        public int Percentages { get; set; }

    }
}
