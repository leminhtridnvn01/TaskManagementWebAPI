using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Users.UpdateUser
{
    public class UpdateUserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}
