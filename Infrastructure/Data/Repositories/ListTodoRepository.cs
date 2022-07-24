using Domain.Entities.Tasks;
using Domain.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ListTodoRepository : Repository<ListTodo>, IListTodoRepository
    {
        public ListTodoRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}