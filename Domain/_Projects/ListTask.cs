using Domain.Base;
using Domain.Entities.Tasks;
using Domain.Projects;
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