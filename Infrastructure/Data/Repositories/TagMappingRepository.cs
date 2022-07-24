using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Tasks;
using Domain.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TagMappingRepository : Repository<TagMapping>, ITagMappingRepository
    {
        public TagMappingRepository(EFContext dbContext) : base(dbContext)
        {
        }

        public Task AddTag(int tagId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Tag>> GetAllTags(int taskId)
        {
            return await DbSet.Where( w => w.Task.Id == taskId ).Where( w => w.Tag.IsDeleted == false).Select( s => s.Tag).ToListAsync();
        }

        public async Task<List<TaskItem>> GetAllTasks(int tagId)
        {
            return await DbSet.Where( w => w.Tag.Id == tagId ).Where( w => w.Task.IsDeleted == false).Select( s => s.Task).ToListAsync();
        }
    }
}