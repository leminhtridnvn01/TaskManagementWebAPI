using Domain.Base;
using Domain.Projects;

namespace Domain.Users.Events
{
    public class CreateDefaultProjectWhenRegisterDomainEvent : BaseDomainEvent
    {
        public User User { get; set; }
        public CreateDefaultProjectWhenRegisterDomainEvent(User user)
        {
            User = user;
        }
    }
}
