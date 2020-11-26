using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Repository.Repository
{
    public interface IBaseRepository
    {
        /// <summary>
        /// DbContext i verir.
        /// </summary> 
        /// <returns></returns>
        DbContext GetDbContext();
    }
}
