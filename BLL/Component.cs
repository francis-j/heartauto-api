using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using MongoDB.Bson;

namespace BLL
{
    public abstract class Component<T> : IComponent<T>, IDisposable
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
                repository.Create(item);
        }

        public void Delete(ObjectId id)
        {
            if (GetById(id) != null)
                repository.Delete(id);
        }

        public IEnumerable<T> Get()
        {
            var list = repository.Read();

            return list;
        }

        public IEnumerable<T> Get(IEnumerable<KeyValuePair<string, object>> filters)
        {
            var list = repository.Read(filters.ToList());

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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Component() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}