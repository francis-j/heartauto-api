using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL
{
    public abstract class MongoRepository<T> : IRepository<T>
    {
        private readonly IErrorLogger logger;

        private MongoClient client;
        private IMongoDatabase database;
        public IMongoCollection<T> collection;

        public MongoRepository(IErrorLogger logger)
        {
            this.logger = logger;

            var connectionString = LookupValues.MONGODB_CONNECTION_STRING;
            this.client = new MongoClient(connectionString);
            this.database = this.client.GetDatabase(LookupValues.MONGODB_COLLECTION_NAME);
            
            this.collection = this.database.GetCollection<T>(typeof(T).Name);
        }

        public virtual IEnumerable<T> Read()
        {
            var items = this.collection.Find(new BsonDocument()).ToList();

            return items;
        }

        public virtual IEnumerable<T> Read(List<KeyValuePair<string, object>> filters)
        {
            try 
            {
                var definitions = new List<FilterDefinition<T>>();

                foreach (var filter in filters)
                {
                    var definition = Builders<T>.Filter.Eq(filter.Key, filter.Value);
                    definitions.Add(definition);
                }

                var masterFilter = Builders<T>.Filter.And(definitions);

                var result = this.collection.Find(masterFilter).ToList();

                return result;
            }
            catch (Exception e)
            {
                this.logger.Write(e.Source, e.Message);
            }
            
            return null;      
        }

        public virtual T Create(T item)
        {
            try 
            {
                this.collection.InsertOneAsync(item);
                
                return item;
            }
            catch (Exception e)
            {
                logger.Write(e.Source, e.Message);
            }

            return default(T);
        }

        public virtual void Delete(ObjectId id)
        {
            try 
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                this.collection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                logger.Write(e.Source, e.Message);
                throw new Exception(e.Message);
            }
        }

        public virtual T Update(ObjectId id, T item)
        {
            try 
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                this.collection.FindOneAndReplaceAsync(filter, item);

                return item;
            }
            catch (Exception e)
            {
                logger.Write(e.Source, e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}