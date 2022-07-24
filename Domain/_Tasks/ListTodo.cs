using Domain.Base;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Tasks
{
    public partial class ListTodo : BaseEntity<int>
    {
        public ListTodo()
        {
            TodoItems = new HashSet<TodoItem>();
        }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public virtual TaskItem TaskItem { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }

        public float CalculateDone()
        {
            float rs = 0;
            float totalSubTodoItems = (float)this.TodoItems.Count();
            foreach (var subItem in this.TodoItems)
            {
                float a = (1 / totalSubTodoItems) * 100;
                if (subItem.IsDone == true) rs = rs + (1 / totalSubTodoItems) * 100;
            }  
            return rs;  
        }
    }
}
