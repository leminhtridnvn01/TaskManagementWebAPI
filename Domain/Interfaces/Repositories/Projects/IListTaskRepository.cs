using Domain.Interfaces;

namespace Domain.ListTasks
{
    public interface IListTaskRepository : IAsyncRepository<ListTask>
    {
    }
}