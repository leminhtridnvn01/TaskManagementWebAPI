using Domain.DTOs.ListTasks.AddListTask;
using Domain.DTOs.Projects.AddProject;
using Domain.DTOs.Projects.GetProject;
using Domain.DTOs.Projects.UpdateProject;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectDetailResponse> GetProject(int projectId);

        Task<ProjectDetailResponse> CreateProject(AddProjectRequest projectRequest);

        Task<ProjectDetailResponse> AddMember(int memberId, int projectId);

        Task<ProjectDetailResponse> CreateListTask(int projectId, AddListTaskRequest listTaskRequest);

        Task<ProjectDetailResponse> UpdateProject(int projectId, UpdateProjectRequest projectRequest);

        Task<bool> DeleteProject(int projectId);

        Task<ProjectDetailResponse> DeleteListTask(int projectId, int listTaskId);

        Task<ProjectDetailResponse> RemoveMember(int memberId, int projectId);
    }
}