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
        T Create(T item);
        void Delete(ObjectId id);
        T Update(ObjectId id, T item);
    }
}
