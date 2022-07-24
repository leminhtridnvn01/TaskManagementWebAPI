using Domain.ListTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ListTaskRepository : Repository<ListTask>, IListTaskRepository
    {
        public ListTaskRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}
