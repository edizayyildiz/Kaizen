using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Kaizen.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext db;
        private readonly DbSet<T> entities;
        private readonly IIdentity identity;
        public Repository(ApplicationDbContext db, IIdentity identity)
        {
            this.db = db;
            this.entities = db.Set<T>();
            this.identity = identity;
        }
        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public T Find(Guid id)
        {
            return entities.FirstOrDefault(f => f.Id == id);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return entities.FirstOrDefault(where);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return entities.Where(where).ToList();
        }

        public void Insert(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = identity.Name;
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = identity.Name;
            entities.Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = identity.Name;
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
