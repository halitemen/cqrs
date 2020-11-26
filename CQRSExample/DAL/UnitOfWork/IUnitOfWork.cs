using CQRSExample.Repository.Repository;
using System;

namespace CQRSExample.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IQueryRepository<T> GetQueryRepository<T>() where T : class;
        ICommandRepository<T> GetCommandRepository<T>() where T : class;
        int SaveChanges();
    }
}
