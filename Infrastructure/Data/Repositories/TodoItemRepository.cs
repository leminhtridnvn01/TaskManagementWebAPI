using Domain.Entities.Tasks;
using Domain.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}