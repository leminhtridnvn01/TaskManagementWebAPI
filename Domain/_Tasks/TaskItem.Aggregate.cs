using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Base;
using Domain.ListTasks;
using Domain.Users;

namespace Domain.Entities.Tasks
{
    public partial class TaskItem : IAggregateRoot
    {
        public TaskItem([NotNull] string name,
            DateTime deadline,
            string prioritized,
            string description, 
            int listTaskId) : this()
        {
            this.Update(name, prioritized, description);
            this.ChangeDeadline(deadline);
            this.AddListTask(listTaskId);        
        }

        public void Update([NotNull] string name,
            string prioritized,
            string description)
        {
            Name = name;
            Prioritized = prioritized;
            Description = description;
        }

        public void AddListTask(int listTaskId)
        {
            ListTaskId = listTaskId;
        }

        public void CreateAttachment(string name, string fileType, string url)
        {
            this.Attachments.Add(new Attachment
            {
                Name = name,
                FileType = fileType,
                URL = url
            });
        }

        public void CreateListTodo(string name)
        {
            this.ListTodoes.Add(new ListTodo
            {
                Name = name
            });
        }

        public void CreateTodoItem(int listTodoId, string name)
        {
            this.ListTodoes.FirstOrDefault(s => s.Id == listTodoId)
                .TodoItems.Add(new TodoItem
                {
                    Name = name
                });
        }
        public void CreateSubTodoItem(int listTodoId, int SubTodoItemId, string name)
        {
            this.ListTodoes.FirstOrDefault(s => s.Id == listTodoId)
                .TodoItems.FirstOrDefault(s => s.Id == SubTodoItemId)
                .SubTodoItems.Add(new TodoItem
                {
                    Name = name
                });
        }
        public void AddAssignment(int assigneeId)
        {
            this.Assignees.Add(new Assignment
            {
                UserId = assigneeId,
                TaskItemId = this.Id
            });
        }

        public void RemoveAssignment(int assigneeId)
        {
            this.Assignees.Remove(this.Assignees.FirstOrDefault(s => s.UserId == assigneeId));
        }

        public void AddTag(int tagId)
        {
            Tags.Add(new TagMapping
            {
                TagId = tagId,
                TaskId = this.Id
            });
        }

        public void RemoveTag(int tagId)
        {
            this.Tags.Remove(this.Tags.FirstOrDefault(s => s.TagId == tagId));
        }

        public bool ChangeDeadline(DateTime newDeadline)
        {
            if (newDeadline.ToUniversalTime() <= DateTime.UtcNow) return false;
            this.Deadline = newDeadline;
            return true;
        }

        public bool ChangeAssigneeInProgress(int assigneeId)
        {
            this.AssigneeInProgressId = assigneeId;
            return true;
        }
    }
}