using Domain.Interfaces;
using Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public interface IProjectMemberRepository : IAsyncRepository<ProjectMember>
    {
        Task AddMember(int memberId, int projectId);

        Task<List<User>> GetAllMember(int projectId);

        Task<List<Project>> GetAllProject(int memberId);
    }
}