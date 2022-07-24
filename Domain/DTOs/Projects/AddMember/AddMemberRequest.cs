using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Projects.AddMember
{
    public class AddMemberRequest
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
    }
}
