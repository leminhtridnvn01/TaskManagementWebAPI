using Domain.Base;

namespace Domain.Entities.Tasks
{
    public partial class TagMapping : BaseEntity<int>
    {
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int TaskId { get; set; }
        public virtual TaskItem Task { get; set; }
    }
}