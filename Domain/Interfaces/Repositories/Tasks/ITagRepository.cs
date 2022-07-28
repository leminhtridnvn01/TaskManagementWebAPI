using Domain.Entities.Tasks;
using Domain.Interfaces;

namespace Domain.Tasks
{
    public interface ITagRepository : IAsyncRepository<Tag>
    {
    }
}