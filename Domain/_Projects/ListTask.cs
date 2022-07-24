using Domain.Base;
using Domain.Projects;
using Domain.Entities.Tasks;
using System.Collections.Generic;

namespace Domain.ListTasks
{
    public partial class ListTask : BaseEntity<int>
    {
        public ListTask()
        {
            TaskItems = new HashSet<TaskItem>();
        }
        public string Name { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
