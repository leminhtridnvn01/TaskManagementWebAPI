using Domain.Entities.Tasks;
using Domain.Interfaces;

namespace Domain.Tasks
{
    public interface IAttachmentRepository : IAsyncRepository<Attachment>
    {
    }
}