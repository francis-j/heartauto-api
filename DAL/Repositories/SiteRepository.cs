using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BE;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class SiteRepository : MongoRepository<Site>
    {
        public SiteRepository(IErrorLogger logger) : base(logger)
        {
            
        }
    }
}