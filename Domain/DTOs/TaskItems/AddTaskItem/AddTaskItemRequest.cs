using System;

namespace Domain.DTOs.TaskItems.AddTaskItem
{
    public class AddTaskItemRequest
    {
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Prioritized { get; set; }
        public string Description { get; set; }
    }
}