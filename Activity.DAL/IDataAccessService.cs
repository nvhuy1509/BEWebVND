using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Activity.DAL.Entity;

namespace Activity.DAL
{

    public interface IDataAccessService
    {
        Task AddRecord<T>(T entity) where T : class;
        Task UpdateRecord<T>(T entity) where T : class;
        Task<IEnumerable<T>> GetALL<T>()where T : class;
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class;
        Task<int> Count<T>(Expression<Func<T, bool>> filterExpression) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task DeleteManyAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class;
        Task<T> GetByIdAsync<T, Tkey>(Tkey id) where T : class;
        Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class;
    }
}
