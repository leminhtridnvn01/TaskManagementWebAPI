﻿using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();

        IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity;

    }
}
