namespace Domain.DTOs.TaskItems.UpdateTaskItem
{
    public class UpdateTaskItemsRequest
    {
        public string Name { get; set; }
        public string Prioritized { get; set; }
        public string Description { get; set; }
    }
}