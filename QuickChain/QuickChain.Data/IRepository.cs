using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QuickChain.Data
{
    public interface IRepository<T>
    {
        T Insert(T entity);
        void Delete(T entity);
        void Update(T entiry);
        void Attach(T entiry);
        void UpdateProperty(T entity, Expression<Func<T, object>> property);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(bool orderByDescending = true);
        T GetById(int id);
        T GetByIdIncluding(int id, Expression<Func<T, object>> includes);
        void Save();
    }
}
