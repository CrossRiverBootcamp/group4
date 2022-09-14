using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DTO
{
    public class EmailVerificationDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string VerificationCode { get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
    }
}
