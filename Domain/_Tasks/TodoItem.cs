using Domain.Base;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Tasks
{
    public partial class TodoItem : BaseEntity<int>
    {
        public TodoItem()
        {
            SubTodoItems = new HashSet<TodoItem>();
        }

        public string Name { get; set; }
        public bool IsDone { get; set; }
        public virtual ListTodo ListTodo { get; set; }
        public virtual TodoItem TodoParrent { get; set; }
        public virtual ICollection<TodoItem> SubTodoItems { get; set; }
    }
}