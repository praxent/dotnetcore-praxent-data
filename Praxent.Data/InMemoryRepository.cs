using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Praxent.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IDataEntity, new()
    {
        private readonly IDictionary<string, T> _dataDictionary;

        public InMemoryRepository()
        {
            _dataDictionary = new ConcurrentDictionary<string, T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dataDictionary.Values.AsQueryable();
        }

        public T GetById(string id)
        {
            return _dataDictionary.TryGetValue(id, out var result) ? result : default(T);
        }

        public T Add(T entity)
        {
            if (_dataDictionary.TryAdd(entity.ID, entity))
            {
                return entity;
            }

            throw new Exception(string.Format("Could not add entity of type {0} with ID {1} to Repository! An Entity with that ID already exists!", nameof(T), entity.ID));
        }

        public T Update(T entity)
        {
            if (_dataDictionary.ContainsKey(entity.ID))
            {
                _dataDictionary[entity.ID] = entity;
                return entity;
            }

            throw new Exception(string.Format("Could not update entity of type {0} with ID {1} in Repository! An Entity with that ID does not exist!", nameof(T), entity.ID));
        }

        public T Delete(T entity)
        {
            if (_dataDictionary.ContainsKey(entity.ID))
            {
                _dataDictionary.Remove(entity.ID);
                return entity;
            }

            throw new Exception(string.Format("Could not delete entity of type {0} with ID {1} in Repository! An Entity with that ID does not exist!", nameof(T), entity.ID));
        }

        public T Delete(string id)
        {
            if (_dataDictionary.ContainsKey(id))
            {
                _dataDictionary.TryGetValue(id, out var entity);
                if (entity != null)
                {
                    _dataDictionary.Remove(entity.ID);
                    return entity;
                }
            }

            throw new Exception(string.Format("Could not delete entity of type {0} with ID {1} in Repository! An Entity with that ID does not exist!", nameof(T), id));
        }
    }
}