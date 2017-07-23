using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;

namespace BLL
{
    public class AccountComponent : Component<Account>
    {
        public AccountComponent(IRepository<Account> repository)
        {
            this.repository = repository;
        }

        public Account Login(Account account)
        {
            var filters = new List<KeyValuePair<string, object>>();
            filters.Add(new KeyValuePair<string, object>(account.Email, account.Password));
            
            var result = (this.repository).Get(filters).ToList().First();

            return result;
        }
    }
}