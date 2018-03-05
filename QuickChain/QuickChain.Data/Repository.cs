using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QuickChain.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected DbContext dbContext;
        protected DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbSet = dbContext.Set<T>();
            this.dbContext = dbContext;
        }

        public T Insert(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            EntityEntry<T> entityEntry = this.dbSet.Add(entity);

            return entityEntry.Entity;
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }

        public void Attach(T entity)
        {
            this.dbSet.Attach(entity);
        }

        public void UpdateProperty(T entity, Expression<Func<T, object>> property)
        {
            this.dbContext
                .Entry(entity)
                .Property(property)
                .IsModified = true;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll(bool orderByDescending = true)
        {
            if (orderByDescending)
            {
                return this.SearchFor(x => true)
                    .OrderByDescending(i => i.CreatedOn);
            }

            return this.SearchFor(x => true);
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetByIdIncluding(int id, Expression<Func<T, object>> includes)
        {
            return dbSet
                .Include(includes)
                .Single(x => x.Id == id);

        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
