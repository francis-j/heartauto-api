using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE
{
    public class Site : GTObject
    {
        public string AccountId { get; set; }
        public IEnumerable<Page> Pages { get; set; }
    }
}