using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [MaxLength(75)]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
