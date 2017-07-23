using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(List<KeyValuePair<string, object>> filters);
        bool Add(T item);
        bool Delete(ObjectId id);
        bool Update(ObjectId id, T item);
    }
}
