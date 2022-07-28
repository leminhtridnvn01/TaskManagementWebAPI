using Domain.Base;
using Domain.Users;

namespace Domain.Projects
{
    public partial class ProjectMember : BaseEntity<int>
    {
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int MemberId { get; set; }
        public virtual User Member { get; set; }
    }
}