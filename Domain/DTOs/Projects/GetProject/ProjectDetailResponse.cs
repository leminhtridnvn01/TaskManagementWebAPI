using Domain.DTOs.ListTasks.GetListTask;
using Domain.Users;
using System.Collections.Generic;

namespace Domain.DTOs.Projects.GetProject
{
    public class ProjectDetailResponse
    {
        public ProjectDetailResponse()
        {
            ListTasks = new List<ListTaskDetailResponse>();
            ListMember = new List<UserResponse>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        // public string UserOwner { get; set; }
        public List<ListTaskDetailResponse> ListTasks { get; set; }

        public List<UserResponse> ListMember { get; set; }
    }
}