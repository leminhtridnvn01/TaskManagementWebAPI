using Domain.Base;
using System;
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

        public float CalculateDone()
        {
            float rs = 0;
            float totalSubTodoItems = (float)this.SubTodoItems.Count();
            foreach (var subItem in this.SubTodoItems)
            {
                float a = (1 / totalSubTodoItems) * 100;
                if (subItem.IsDone == true) rs = rs + (1 / totalSubTodoItems) * 100;
            }  
            return rs;  
        }
    }
}
