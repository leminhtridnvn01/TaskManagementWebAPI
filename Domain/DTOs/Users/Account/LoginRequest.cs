using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Users
{
    public class LoginRequest
    {
        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}