using Domain.Entities.Tasks;
using Domain.Interfaces;

namespace Domain.Tasks
{
    public interface IListTodoRepository : IAsyncRepository<ListTodo>
    {
         
    }
}