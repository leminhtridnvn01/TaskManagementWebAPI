using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Users
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public string Address { get; set; }
    }
}