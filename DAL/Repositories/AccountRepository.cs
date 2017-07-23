using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BE;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(IErrorLogger logger) : base(logger)
        {
            
        }
    }
}