using Domain.Entities.Tasks;
using Domain.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}