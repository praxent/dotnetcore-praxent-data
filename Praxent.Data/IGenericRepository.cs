using System;
using System.Linq;

namespace Praxent.Data
{
    public interface IGenericRepository
    {
        IQueryable<object> GetAll(Type type);
        object GetById(Type type, string id);
        object Add(Type type, object entity);
        object Update(Type type, object entity);
        object Delete(Type type, object entity);
        object Delete(Type type, string id);
    }
}