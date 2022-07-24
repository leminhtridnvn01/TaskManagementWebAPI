using System.Collections.Generic;

namespace Domain.DTOs.TodoItems.GetTodoItem
{
    public class TodoItemResponse
    {
        public TodoItemResponse()
        {
            SubTodoItems = new List<TodoItemResponse>();
        }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public float Done { get; set; }
        public List<TodoItemResponse> SubTodoItems { get; set; }
    }
}