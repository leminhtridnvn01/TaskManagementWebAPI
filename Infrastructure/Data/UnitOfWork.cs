using Domain._Histories;
using Domain.Base;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;
        private readonly IMediator _mediator;
        public UnitOfWork(EFContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_dbContext);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            _dbContext.OnBeforeSaveChanges();
            await _dbContext.SaveChangesAsync();
            await _dbContext.SaveEntitiesAsync();
            return true;
        }
        
    }
}
