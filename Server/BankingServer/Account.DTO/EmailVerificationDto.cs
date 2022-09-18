using System.ComponentModel.DataAnnotations;

namespace Account.DTO
{
    public class EmailVerificationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }
    }
}
