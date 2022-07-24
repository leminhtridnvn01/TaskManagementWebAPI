using Domain.Projects;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProjectMemberRepository : Repository<ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(EFContext dbContext) : base(dbContext)
        {
        }

        public Task AddMember(int memberId, int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllMember(int projectId)
        {
            return await DbSet.Where( w => w.Project.Id == projectId ).Where( w => w.Member.IsDeleted == false).Select( s => s.Member).ToListAsync();
        }

        public async Task<List<Project>> GetAllProject(int memberId){
            return await DbSet.Where( w => w.Member.Id == memberId ).Where( w => w.Project.IsDeleted == false).Select( s => s.Project).ToListAsync();
        }
    }
}
