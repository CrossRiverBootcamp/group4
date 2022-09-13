
using System.ComponentModel.DataAnnotations;


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
