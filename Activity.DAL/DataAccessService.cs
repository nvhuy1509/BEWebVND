using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Activity.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Activity.DAL
{
    public class DataAccessService: IDataAccessService
    {
        private  PostgreSqlContext _context;
        private PostgreSqlContext getContent()
        {
            if (_context != null) return _context;
            else
            {
                _context = new PostgreSqlContext();
                return _context;
            }
        }

        public async Task AddRecord<T>(T entity) where T : class
        {
            try
            {
                getContent().ChangeTracker.Clear();
                getContent().Set<T>().Add(entity);
                await getContent().SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddRecord=>" + ex.InnerException.Message);
                Debug.WriteLine("AddRecord=>" + ex.Message);
            }
           
        }

        public void DeleteRecord<T>(string id) where T : class
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<T>> GetALL<T>() where T : class
        {
            IQueryable<T> queryable = getContent().Set<T>();

            var lst = await queryable.AsNoTracking().ToListAsync();
            return lst;
        }
        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class
        {
                IQueryable<T> queryable = getContent().Set<T>();
                var skip = 0;
                var limit = 0;

                if (filterExpression != null)
                {
                    queryable = queryable.Where(filterExpression);
                }

                if (skip > 0)
                {
                    queryable = queryable.Skip(skip);
                }

                if (limit > 0)
                {
                    queryable = queryable.Take(limit);
                }

                var lst = await queryable.ToListAsync();
                
                return lst;

        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> filterExpression) where T : class
        {
            IQueryable<T> queryable = getContent().Set<T>();

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            int count = queryable.Count();
            return count;
        }

        public async Task UpdateRecord<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                getContent().ChangeTracker.Clear();
                getContent().Entry(entity).State = EntityState.Modified;
                await getContent().SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

           
        }

       
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            var dbSet = getContent().Set<T>();
            dbSet.Remove(entity);
            await getContent().SaveChangesAsync();
        }

        public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class
        {

            var dbSet = getContent().Set<T>();
            await dbSet.Where(filterExpression).DeleteAsync();
        }

        public async Task<T> GetByIdAsync<T, Tkey>(Tkey id) where T : class
        {
            getContent().ChangeTracker.Clear();
            var obj = await getContent().Set<T>().FindAsync(id);
            return obj;
        }

        public async Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : class
        {
            getContent().ChangeTracker.Clear();
            IQueryable<T> queryable = getContent().Set<T>();
            queryable = queryable.Where(filterExpression);

            var obj =  await queryable.FirstOrDefaultAsync();
            return obj;
        }
    }
}
