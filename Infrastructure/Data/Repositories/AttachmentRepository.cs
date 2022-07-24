using Domain.Entities.Tasks;
using Domain.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}