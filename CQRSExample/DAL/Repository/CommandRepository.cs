using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSExample.Repository.Repository
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly DbContext DbContext;
        private readonly DbSet<T> DbSet;

        /// <summary>
        /// Repository instance ı başlatırç
        /// </summary>
        /// <param name="dbContext">Veritabanı bağlantı nesnesi</param>
        public CommandRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }


        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Aynı kayıt eklememek için objeyi kontrol ederek true veya false dönderir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }
        public void Delete(T entity, bool forceDelete = false)
        {
            // Önce entity'nin state'ini kontrol etmeliyiz.
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false)
        {
            Delete(DbSet.First(predicate), forceDelete);
        }

        public DbContext GetDbContext()
        {
            return DbContext;
        }

        /// <summary>
        /// Verilen veriyi context üzerinde günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek entity</param>
        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(Expression<Func<T, bool>> predicate, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
