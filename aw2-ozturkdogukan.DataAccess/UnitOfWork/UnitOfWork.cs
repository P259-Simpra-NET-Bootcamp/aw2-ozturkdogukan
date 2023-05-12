using aw2_ozturkdogukan.Data.Context;
using aw2_ozturkdogukan.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace aw2_ozturkdogukan.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Aw2DbContext DbContext;
        private bool disposed = false;

        public UnitOfWork(Aw2DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(DbContext);
        }

        public int SaveChanges()
        {
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    int result = DbContext.SaveChanges();
                    tScope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
