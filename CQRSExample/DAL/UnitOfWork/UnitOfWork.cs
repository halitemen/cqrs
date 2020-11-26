using CQRSExample.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Transactions;

namespace CQRSExample.DAL.UnitOfWork
{
    public class UnitOfWork<T> : IDisposable, IUnitOfWork where T : DbContext
    {
        #region Constructor

        /// <summary>
        /// UnitOfWork başlangıcı 
        /// </summary>
        public UnitOfWork()
        {

        }

        #endregion

        #region Members

        private DbContext dbContext;
        private bool disposed = false;

        /// <summary>
        /// İşlemlerde hata oluşusa bu liste doldurulur.
        /// </summary>
        public readonly List<string> ErrorMessageList = new List<string>();

        #endregion

        #region Properties

        /// <summary>
        /// Açılan veri bağlantısı.
        /// </summary>
        private DbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = (DbContext)Activator.CreateInstance(typeof(T));
                }
                return dbContext;
            }
            set { dbContext = value; }
        }

        #endregion

        #region IUnitOfWork Members


        /// <summary>
        /// Değişiklikleri kaydet.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            int result = -1;
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    result = DbContext.SaveChanges();
                    tScope.Complete();
                }
            }
            catch (ValidationException ex)
            {
                string errorString = ex.Message;
                ErrorMessageList.Add(errorString);
            }
            catch (DbUpdateException ex)
            {
                string errorString = ex.Message;
                if (ex.InnerException != null)
                {
                    errorString += ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                    {
                        errorString += ex.InnerException.InnerException.Message;
                    }
                }

                ErrorMessageList.Add(errorString);
            }
            catch (Exception ex)
            {
                ErrorMessageList.Add(ex.Message);
            }
            finally
            {
                if (result == -1)
                {
                    //ElasticLogger.Instance.Info(
                    //    $"UnitOfWork Save Error. Type : {typeof(T).Name} Error Messages : {JsonConvert.SerializeObject(ErrorMessageList)}");
                }
            }
            return result;
        }

        #endregion

        public IQueryRepository<T> GetQueryRepository<T>() where T : class
        {
            return new QueryRepository<T>(DbContext);
        }

        public ICommandRepository<T> GetCommandRepository<T>() where T : class
        {
            return new CommandRepository<T>(DbContext);
        }

        #region IDisposable Members



        public void Dispose()
        {
            DbContext.Database.CloseConnection();
            DbContext = null;
        }

        #endregion
    }
}
