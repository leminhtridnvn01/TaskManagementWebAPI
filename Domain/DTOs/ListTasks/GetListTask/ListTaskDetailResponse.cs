using Domain.DTOs.TaskItems.GetTaskItem;
using System.Collections.Generic;

namespace Domain.DTOs.ListTasks.GetListTask
{
    public class ListTaskDetailResponse
    {
        public string Name { get; set; }
        public List<TaskItemResponse> TaskItems { get; set; }
    }
}