﻿using Domain.ListTasks;
using Domain.Projects.Events;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Domain.Projects
{
    public partial class Project
    {
        public Project([NotNull] string name
            , string description) : this()
        {
            this.Update(name, description);
        }

        public void Update([NotNull] string name,
            string description)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name can not be null or white space");
            }
            Name = name;
            Description = description;
        }

        public void AddMember(int memberId)
        {
            this.Members.Add(new ProjectMember
            {
                MemberId = memberId,
                ProjectId = this.Id
            });
        }

        public void CreateListTaskDefault()
        {
            this.ListTasks.Add(new ListTask { Name = "Cơ hội" });
            this.ListTasks.Add(new ListTask { Name = "Báo giá" });
            this.ListTasks.Add(new ListTask { Name = "Đơn hàng" });
            this.ListTasks.Add(new ListTask { Name = "Hoàn thành" });
        }

        public void CreateListTask(string listTaskName)
        {
            this.ListTasks.Add(new ListTask
            {
                Name = listTaskName
            });
        }

        public void RemoveMember(int memberId)
        {
            this.Members.Remove(this.Members.FirstOrDefault(s => s.MemberId == memberId));
        }

        public void DeleteListTask(int listTaskId)
        {
            var deleteListTaskDomainEvent = new DeleteListTaskDomainEvent(listTaskId);
            this.AddEvent(deleteListTaskDomainEvent);
        }
    }
}