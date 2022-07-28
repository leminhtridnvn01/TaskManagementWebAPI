using Domain.Projects;
using Domain.Users.Events;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Users
{
    public partial class User
    {
        public User()
        {
            Projects = new HashSet<ProjectMember>();
        }

        public User([NotNull] string name, [NotNull] int age, [NotNull] string address)
        {
            this.Update(name, age, address);
        }

        public void Update([NotNull] string name, [NotNull] int age, [NotNull] string address)
        {
            Name = name;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void CreateDefaultProject()
        {
            var createDefaultProjectWhenRegisterDomainEvent = new CreateDefaultProjectWhenRegisterDomainEvent(this);
            this.AddEvent(createDefaultProjectWhenRegisterDomainEvent);
        }
    }
}