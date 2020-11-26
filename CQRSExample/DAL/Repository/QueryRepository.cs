using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSExample.Repository.Repository
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {

        private readonly DbContext DbContext;
        private readonly DbSet<T> DbSet;

        /// <summary>
        /// Repository instance ı başlatırç
        /// </summary>
        /// <param name="dbContext">Veritabanı bağlantı nesnesi</param>
        public QueryRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public DbContext GetDbContext()
        {
            return DbContext;
        }

        public int Count()
        {
            return Count(arg => true);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
            .Where(predicate);
            return iQueryable.Count();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
                .Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = DbSet.Where(x => x != null);
            return iQueryable;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
                .Where(predicate);
            return iQueryable;
        }

    }
}
