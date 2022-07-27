using System;
using System.Threading.Tasks;
using Domain.DTOs.Attachments.AddAttachment;
using Domain.DTOs.Attachments.GetAttachment;
using Domain.DTOs.ListTasks.GetListTask;
using Domain.DTOs.ListTodos.AddListTodo;
using Domain.DTOs.Tags.DeleteTag;
using Domain.DTOs.TaskItems.AddTaskItem;
using Domain.DTOs.TaskItems.GetTaskItem;
using Domain.DTOs.TaskItems.UpdateTaskItem;
using Domain.DTOs.TodoItems.AddTodoItem;
using Domain.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ITaskItemService
    {
        Task<TaskItemDetailResponse> GetTaskItem(int taskItemId);
        Task<ListTaskDetailResponse> CreateTaskItem(int listTaskId, AddTaskItemRequest taskItemInput);
        Task<TaskItemDetailResponse> CreateListTodo(int taskItemId, AddListTodoRequest listTodoInput);
        Task<TaskItemDetailResponse> CreateTodoItem(int listTodoId, int todoItemParrentId, AddTodoItemRequest todoItemInput);
        Task<TaskItemDetailResponse> CreateAttachment(int taskItemId, AddAttachmentRequest attachmentInput);
        Task<TaskItemDetailResponse> AddAssignee(int taskItemId, int assigneeId);
        Task<TaskItemDetailResponse> AddTag(int taskItemId, int tagId);
        Task<TaskItemDetailResponse> UpdateTaskItem(int taskItemId, UpdateTaskItemsRequest taskItemInput);
        Task<TaskItemDetailResponse> UpdateDeadlineInTaskItem(int taskItemId, DateTime newDeadline);
        Task<TaskItemDetailResponse> UpdateAssigneeInProgressInTaskItem(int taskItemId, string assigneeUsername);
        Task<bool> DeleteTodoItem(int todoItemId);
        Task<bool> DeleteListTodo(int listTodoId);
        Task<bool> DeleteAssignee(int taskItemId, int assigneeId);
        Task<bool> DeleteAttachment(int attachmentId);
        Task<bool> DeleteTag(int taskItemId, int tagId);
        Task<bool> DeleteTaskItem(int taskItemId);

    }
}