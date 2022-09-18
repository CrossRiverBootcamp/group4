using System.ComponentModel.DataAnnotations;


namespace Account.DTO
{
    public class CustomerDTO
    {
        [MinLength(2)]
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(75)]
        [Required]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(5)]
        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
    }
}
