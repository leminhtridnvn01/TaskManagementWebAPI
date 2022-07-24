using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Tasks;
using Domain.Interfaces;
using Domain.Users;

namespace Domain.Tasks
{
    public interface ITagMappingRepository : IAsyncRepository<TagMapping>
    {
        Task AddTag(int tagId, int taskId);
        Task<List<Tag>> GetAllTags(int taskId);
        Task<List<TaskItem>> GetAllTasks(int tagId);
    }
}