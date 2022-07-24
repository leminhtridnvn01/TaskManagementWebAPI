using Domain.Base;

namespace Domain.Entities.Tasks
{
    public partial class TagMapping : BaseEntity<int>
    {
        public virtual Tag Tag { get; set; }
        public virtual TaskItem Task { get; set; }
    }
}
