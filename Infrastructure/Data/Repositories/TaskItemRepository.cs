using Domain.Entities.Tasks;
using Domain.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}