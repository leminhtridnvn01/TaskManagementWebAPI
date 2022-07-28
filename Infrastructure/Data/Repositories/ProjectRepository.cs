using Domain.Projects;

namespace Infrastructure.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}