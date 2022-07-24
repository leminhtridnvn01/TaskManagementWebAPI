using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Users
{
    public class AccountResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
