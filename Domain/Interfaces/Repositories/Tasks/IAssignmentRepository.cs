using Domain.Entities.Tasks;
using Domain.Interfaces;
using Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public interface IAssignmentRepository : IAsyncRepository<Assignment>
    {
        Task<List<User>> GetAllAssignees(int taskId);

        Task<List<TaskItem>> GetAllTasks(int assigneeId);
    }
}