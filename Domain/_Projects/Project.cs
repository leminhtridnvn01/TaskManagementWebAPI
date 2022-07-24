using Domain.Base;
using Domain.ListTasks;
using System.Collections.Generic;

namespace Domain.Projects
{
    public partial class Project : BaseEntity<int>
    {
        public Project()
        {
           ListTasks = new HashSet<ListTask>();
           Members = new HashSet<ProjectMember>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ListTask> ListTasks { get; set; }
        public virtual ICollection<ProjectMember> Members { get; set; }
    }
}
