using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> Read();
        IEnumerable<T> Read(List<KeyValuePair<string, object>> filters);
        bool Create(T item);
        bool Delete(ObjectId id);
        bool Update(ObjectId id, T item);
    }
}
