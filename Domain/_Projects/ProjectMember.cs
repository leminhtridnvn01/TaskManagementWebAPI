using Domain.Base;
using Domain.Users;

namespace Domain.Projects
{
    public partial class ProjectMember : BaseEntity<int>
    {
        public virtual Project Project { get; set; }
        public virtual User Member { get; set; }
    }
}
