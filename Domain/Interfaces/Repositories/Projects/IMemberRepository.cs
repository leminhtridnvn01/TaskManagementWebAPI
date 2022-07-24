using Domain.Users;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public interface IMemberRepository : IAsyncRepository<User>
    {
    }
}
