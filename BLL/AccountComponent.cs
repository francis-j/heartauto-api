using System;
using System.Collections.Generic;
using System.Linq;
using BE.AccountEntities;
using DAL;

namespace BLL
{
    public class AccountComponent : Component<Account>
    {
        public AccountComponent(IRepository<Account> repository)
        {
            this.repository = repository;
        }

        public Account Login(AccountLogin account)
        {
            var filters = new List<KeyValuePair<string, object>>();
            filters.Add(new KeyValuePair<string, object>("Email", account.Email));
            filters.Add(new KeyValuePair<string, object>("Password", account.Password));
            
            var result = (this.repository).Read(filters).ToList().First();

            return result;
        }
    }
}