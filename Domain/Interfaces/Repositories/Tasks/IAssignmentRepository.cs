using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Tasks;
using Domain.Interfaces;
using Domain.Users;

namespace Domain.Tasks
{
    public interface IAssignmentRepository : IAsyncRepository<Assignment>
    {
        Task<List<User>> GetAllAssignees(int taskId);
        Task<List<TaskItem>> GetAllTasks(int assigneeId);
    }
}