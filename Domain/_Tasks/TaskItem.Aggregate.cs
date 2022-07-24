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
            string description) : this()
        {
            this.Update(name, prioritized, description);
            this.ChangeDeadline(deadline);
        }

        public void Update([NotNull] string name,
            string prioritized,
            string description)
        {
            Name = name;
            Prioritized = prioritized;
            Description = description;
        }

        public void AddListTask(ListTask listTask)
        {
            ListTask = listTask;
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
        public void AddAssignment(User assignee)
        {
            this.Assignees.Add(new Assignment
            {
                User = assignee,
                TaskItem = this
            });
        }

        public void AddTag(Tag tag)
        {
            Tags.Add(new TagMapping
            {
                Tag = tag,
                Task = this
            });
        }

        public bool ChangeDeadline(DateTime newDeadline)
        {
            if (newDeadline.ToUniversalTime() <= DateTime.UtcNow) return false;
            this.Deadline = newDeadline;
            return true;
        }

        public bool ChangeAssigneeInProgress(User assignee)
        {
            this.AssigneeInProgress = assignee;
            return true;
        }
    }
}