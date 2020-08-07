using System.Linq;

namespace Praxent.Data
{
    public interface ICompositeKeyRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        T GetByKey(params object[] keyValues);
        T Add(T entity);
        T Update(T entity, params object[] keyValues);
        T Delete(params object[] keyValues);
    }
}