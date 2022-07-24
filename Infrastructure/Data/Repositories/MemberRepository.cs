using Domain.Users;
using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class MemberRepository : Repository<User>, IMemberRepository
    {
        public MemberRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
