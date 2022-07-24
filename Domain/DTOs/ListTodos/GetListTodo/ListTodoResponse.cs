using System.Collections.Generic;
using Domain.DTOs.TodoItems.GetTodoItem;

namespace Domain.DTOs.ListTodos.GetListTodo
{
    public class ListTodoResponse
    {
        public ListTodoResponse()
        {
            TodoItems = new List<TodoItemResponse>();
        }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public float Done { get; set; }
        public List<TodoItemResponse> TodoItems { get; set; }
    }
}