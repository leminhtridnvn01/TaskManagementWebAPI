using Domain.Projects;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
    }
}
