using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Praxent.Data
{
    /*
     * Here there be dragons... don't embrace the Service Locator (Anti)Pattern!
     * We're using it here with Reflection to do potentially dangerous things!
     * PROCEED WITH CAUTION!
     */
    public class GenericRepository : IGenericRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryable<object> GetAll(Type type)
        {
            var method = GetType().GetMethod("GetAllTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new object[0]) as IQueryable<object>;
        }

        public object GetById(Type type, string id)
        {
            var method = GetType().GetMethod("GetByIdTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new object[] { id });
        }

        public object Add(Type type, object entity)
        {
            var method = GetType().GetMethod("AddTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new[] { entity });
        }

        public object Update(Type type, object entity)
        {
            var method = GetType().GetMethod("UpdateTyped");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new[] { entity });
        }

        public object Delete(Type type, object entity)
        {
            var method = GetType().GetMethod("DeleteTypedByEntity");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new[] { entity });
        }

        public object Delete(Type type, string id)
        {
            var method = GetType().GetMethod("DeleteTypedById");
            var generic = method.MakeGenericMethod(type);

            return generic.Invoke(this, new object[] { id });
        }

        public IQueryable<T> GetAllTyped<T>() where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.GetAll();
        }

        public T GetByIdTyped<T>(string id) where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.GetById(id);
        }

        public T AddTyped<T>(T entity) where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.Add(entity);
        }

        public T UpdateTyped<T>(T entity) where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.Update(entity);
        }

        public T DeleteTypedByEntity<T>(T entity) where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.Delete(entity);
        }

        public T DeleteTypedById<T>(string id) where T : class, IDataEntity, new()
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<T>>();

            return repository.Delete(id);
        }
    }
}