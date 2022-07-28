using Domain.Interfaces;
using Domain.Users;

namespace Domain.Projects
{
    public interface IMemberRepository : IAsyncRepository<User>
    {
    }
}