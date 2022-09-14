using System.ComponentModel.DataAnnotations;


namespace Account.DAL.Entities
{
    public class EmailVerificationEntity
    {
        [Key]
        public string Email { get; set; }
        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
    }
}
