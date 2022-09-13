using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Account.DAL.Entities
{
    public class CustomerEntity
    {
        [Key]
        [Index(IsUnique = true)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
