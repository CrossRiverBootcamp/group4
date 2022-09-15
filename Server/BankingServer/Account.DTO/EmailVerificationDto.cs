using System.ComponentModel.DataAnnotations;

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
