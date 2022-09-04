using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Account.DAL.Entities
{
    public class AccountEntity
    {
        [Key]
        [Index(IsUnique = true)]
        [Required]
        public int Id { get; set; }
        [ForeignKey("CustomerEntity")]
        public int CostomerId { get; set; }
        public CustomerEntity Costomer { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public float Balance { get; set; }
    }
}
