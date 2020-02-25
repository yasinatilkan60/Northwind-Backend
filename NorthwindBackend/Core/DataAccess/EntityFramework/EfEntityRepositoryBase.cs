using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> // T -> Type
    where TEntity : class, IEntity, new() // TEntity new'lenebilir ve IEntity implementini içersin.
    where TContext : DbContext, new() // // TContext new'lenebilir ve IEntity implementini içersin.
    {
        public void Add(TEntity entity)
        {
            // Using disposable pattern'dır. Yani işi garbage collector'a bırakmaz. Using sonlandığında nesneyi bellekten siler.
            using (var context = new TContext())  // EF eklemesi gerektiğini bilir.
            {
                var addedEntity = context.Entry(entity); // Gönderilen entity'e context'i abone et.
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext()) // EF silmesi gerektiğini bilir.
            {
                var deletedEntity = context.Entry(entity); // Gönderilen entity'e context'i abone et.
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); // Set abone olma operasyonudur. Filtreye göre o datanın gelmesi sağlanır.
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList(); // Filtreye göre liste getirsin, filter yoksa hepsini getirsin.
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext()) // EF güncellemesi gerektiğini bilir.
            {
                var updatedEntity = context.Entry(entity); // Gönderilen entity'e context'i abone et.
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
