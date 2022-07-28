using Domain.Projects;
using Domain.Users;

namespace Infrastructure.Data.Repositories
{
    public class MemberRepository : Repository<User>, IMemberRepository
    {
        public MemberRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}