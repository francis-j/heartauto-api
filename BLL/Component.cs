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

        public virtual T Add(T item)
        {
            foreach (var i in Get().ToList())
            {
                if (i.Equals(item))
                    throw new Exception("Item already exists.");
            }
                
            return repository.Create(item);
        }

        public virtual bool Delete(ObjectId id)
        {
            if (GetById(id) != null) 
            {
                repository.Delete(id);

                return true;
            }
            else 
            {
                throw new Exception("Item does not exist.");
            }
        }

        public virtual IEnumerable<T> Get()
        {
            var list = repository.Read();

            return list;
        }

        public virtual IEnumerable<T> Get(IEnumerable<KeyValuePair<string, object>> filters)
        {
            var list = repository.Read(filters.ToList());

            return list;
        }

        public virtual T GetById(ObjectId id)
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

        public virtual T Update(ObjectId id, T item)
        {
            if (GetById(id) != null)
                return repository.Update(id, item);
            else
                throw new Exception("Item does not exist.");

            throw new Exception("An unspecified error occurred.");
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