using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSExample.Repository.Repository
{
    public interface IQueryRepository<T> : IBaseRepository where T: class
    {
        /// <summary>
        /// Tüm veriyi getir.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Veriyi Where metodu ile getir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verilen sorguya göre tablodaki sayıyı gönderir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Verilen sorguya göre tablodaki sayıyı gönderir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// İstenilen veriyi single olarak getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);
    }
}
