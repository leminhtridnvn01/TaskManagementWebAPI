using Domain.Base;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;
using MediatR;
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