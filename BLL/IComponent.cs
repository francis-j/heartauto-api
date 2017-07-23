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
        void Add (T item);
        void Delete (ObjectId id);
        void Update(ObjectId id, T item);
    }
}