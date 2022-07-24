using System.Collections.Generic;
using Domain.DTOs.Projects.GetProject;

namespace Domain.DTOs.Users
{
    public class UserDetailResponse
    {
        public UserDetailResponse()
        {
            ListProject = new List<ProjectResponse>();
        }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
        public List<ProjectResponse> ListProject { get; set; }
    }
}