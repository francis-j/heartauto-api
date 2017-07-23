using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using MongoDB.Bson;

namespace BLL
{
    public abstract class Component<T> : IComponent<T>
    {
        public IRepository<T> repository;

        public void Add(T item)
        {
            bool performAdd = true;
            foreach (var i in Get().ToList())
            {
                if (i.Equals(item))
                    performAdd = false;
            }

            if (performAdd)
                repository.Add(item);
        }

        public void Delete(ObjectId id)
        {
            if (GetById(id) != null)
                repository.Delete(id);
        }

        public IEnumerable<T> Get()
        {
            var list = repository.Get();

            return list;
        }

        public IEnumerable<T> Get(IEnumerable<KeyValuePair<string, object>> filters)
        {
            var list = repository.Get(filters.ToList());

            return list;
        }

        public T GetById(ObjectId id)
        {
            var filters = new List<KeyValuePair<string, object>>();
            filters.Add(new KeyValuePair<string, object>("_id", id));

            var items = Get(filters);

            if (items.Count() > 0)
            {
                var item = items.ToList().FirstOrDefault();
                return item;
            }

            return default(T);
        }

        public void Update(ObjectId id, T item)
        {
            if (GetById(id) != null)
                repository.Update(id, item);
        }
    }
}