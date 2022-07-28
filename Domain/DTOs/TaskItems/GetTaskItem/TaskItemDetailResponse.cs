using Domain.DTOs.Attachments.GetAttachment;
using Domain.DTOs.ListTodos.GetListTodo;
using Domain.DTOs.Tags.GetTag;
using Domain.Users;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.TaskItems.GetTaskItem
{
    public class TaskItemDetailResponse
    {
        public TaskItemDetailResponse()
        {
            ListTag = new List<TagResponse>();
            ListAssignee = new List<UserResponse>();
            Attachments = new List<AttachmentResponse>();
            ListTodoes = new List<ListTodoResponse>();
        }

        public string Name { get; set; }
        public List<UserResponse> ListAssignee { get; set; }
        public UserResponse AssigneeInProgress { get; set; }
        public DateTime Deadline { get; set; }
        public string Prioritized { get; set; }
        public List<TagResponse> ListTag { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public List<AttachmentResponse> Attachments { get; set; }
        public List<ListTodoResponse> ListTodoes { get; set; }
    }
}