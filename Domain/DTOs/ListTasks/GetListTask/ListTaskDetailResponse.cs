using System.Collections.Generic;
using Domain.DTOs.TaskItems.GetTaskItem;

namespace Domain.DTOs.ListTasks.GetListTask
{
    public class ListTaskDetailResponse
    {
        public string Name { get; set; }
        public List<TaskItemResponse> TaskItems { get; set; }
    }
}