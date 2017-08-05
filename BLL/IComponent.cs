using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace BLL
{
    public interface IComponent<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(IEnumerable<KeyValuePair<string, object>> filters);
        T GetById(ObjectId id);
        T Add (T item);
        bool Delete (ObjectId id);
        T Update(ObjectId id, T item);
    }
}