using System;

namespace Domain.DTOs.TaskItems.GetTaskItem
{
    public class TaskItemResponse
    {
        public string Name { get; set; }
        public string AssigneeInProgress { get; set; }
        public DateTime Deadline { get; set; }
    }
}