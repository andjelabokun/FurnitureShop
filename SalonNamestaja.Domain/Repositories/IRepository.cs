using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T? GetById(params object[] keyValues);
        void Update(T entity);
        void Add(T entity);
        void Remove(T entity);  
    }
}
