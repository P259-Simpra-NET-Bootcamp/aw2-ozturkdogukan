using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace aw2_ozturkdogukan.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext DbContext;
        private readonly DbSet<T> DbSet;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            entity.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);
            entity.GetType().GetProperty("CreatedBy").SetValue(entity, "aw2-ozturkdogukan@sim.com");
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        // Verilen şarta göre tekil olarak veri getirir.
        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet.Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = DbSet.AsQueryable();
            return iQueryable;
        }

        // Verilen şarta göre verileri getirir.
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet;
            iQueryable = iQueryable.Where(predicate);
            return iQueryable;
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }



    }
}
