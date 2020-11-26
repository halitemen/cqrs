using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace CQRSExample.Repository.Repository
{
    public interface ICommandRepository<T> : IBaseRepository where T : class
    {

        /// <summary>
        /// Verilen entityi ekle.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Verilen entity i güncelle.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// predicate göre veriler düzenlenir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        void Update(Expression<Func<T, bool>> predicate, T entity);

        /// <summary>
        /// Verilen entityi sil.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity, bool forceDelete = false);

        /// <summary>
        /// predicate göre veriler silinir.
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false);


        /// <summary>
        /// Aynı kayıt eklememek için objeyi kontrol ederek true veya false dönderir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> predicate);
    }
}
