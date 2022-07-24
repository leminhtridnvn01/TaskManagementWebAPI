using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
