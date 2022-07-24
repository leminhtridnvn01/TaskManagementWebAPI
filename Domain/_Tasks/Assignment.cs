using Domain.Base;
using Domain.Users;

namespace Domain.Entities.Tasks
{
    public partial class Assignment : BaseEntity<int>
    {
        public virtual User User { get; set; }
        public virtual TaskItem TaskItem { get; set; }
    }
}
