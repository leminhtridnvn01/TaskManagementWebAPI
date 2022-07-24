using Domain.Base;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<bool> SoftDeleteAsync(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);

        Task<int> InsertAndGetIdAsync(T entity, bool saveChanges = true);

        Task<IList<TType>> Get<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class;
    
        Task<User> GetUserCreate(T entity);
    }
}
