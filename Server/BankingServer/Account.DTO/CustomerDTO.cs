using System.ComponentModel.DataAnnotations;


namespace Account.DTO
{
    public class CustomerDTO
    {
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(75)]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
        
        public string VerificationCode { get; set; }
    }
}
