using Domain.Base;
using Domain.Projects;
using System.Collections.Generic;

namespace Domain.Users
{
    public partial class User : BaseEntity<int>
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
        public virtual ICollection<ProjectMember> Projects { get; set; }
    }
}