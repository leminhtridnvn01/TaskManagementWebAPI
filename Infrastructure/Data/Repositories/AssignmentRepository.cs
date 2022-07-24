using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Tasks;
using Domain.Tasks;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(EFContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<User>> GetAllAssignees(int taskId)
        {
            return await DbSet.Where( w => w.TaskItem.Id == taskId ).Where( w => w.User.IsDeleted == false).Select( s => s.User).ToListAsync();
        }

        public async Task<List<TaskItem>> GetAllTasks(int assigneeId)
        {
            return await DbSet.Where( w => w.User.Id == assigneeId ).Where( w => w.TaskItem.IsDeleted == false).Select( s => s.TaskItem).ToListAsync();
        }
    }
}