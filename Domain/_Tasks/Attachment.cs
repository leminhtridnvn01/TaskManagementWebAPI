using Domain.Base;

namespace Domain.Entities.Tasks
{
    public partial class Attachment : BaseEntity<int>
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string URL { get; set; }
        public virtual TaskItem TaskItem { get; set; }
    }
}
