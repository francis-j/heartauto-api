using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE
{
    public abstract class GTObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
