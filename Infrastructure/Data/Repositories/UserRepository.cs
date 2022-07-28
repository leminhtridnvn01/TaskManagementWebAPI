using Domain.Users;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}