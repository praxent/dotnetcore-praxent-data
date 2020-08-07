using System.Linq;

namespace Praxent.Data
{
    public interface IRepository<T> where T : class, IDataEntity, new()
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Delete(string id);
    }
}