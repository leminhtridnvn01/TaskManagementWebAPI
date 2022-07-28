using Domain.ListTasks;

namespace Infrastructure.Data.Repositories
{
    public class ListTaskRepository : Repository<ListTask>, IListTaskRepository
    {
        public ListTaskRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}