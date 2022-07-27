using Domain.Base;
using Domain.ListTasks;
using Domain.Users;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Tasks
{
    public partial class TaskItem : BaseEntity<int>
    {
        public TaskItem()
        {
            Assignees = new HashSet<Assignment>();
            Tags = new HashSet<TagMapping>();
            Attachments = new HashSet<Attachment>();
            ListTodoes = new HashSet<ListTodo>();
        }
        public string Name { get; set; }
        public virtual ICollection<Assignment> Assignees { get; set; }
        public int? AssigneeInProgressId { get; set; }
        public virtual User AssigneeInProgress { get; set; }
        public DateTime Deadline { get; set; }
        public string Prioritized { get; set; }
        public virtual ICollection<TagMapping> Tags { get;  set; }
        public string Description { get; set; }
        public int ListTaskId { get; set; }
        public virtual ListTask ListTask { get; set; }
        public bool IsDone { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<ListTodo> ListTodoes { get; set; }
    }
}
