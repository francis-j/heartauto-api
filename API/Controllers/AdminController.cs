using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using BE;
using BE.AccountEntities;
using BLL;
using DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace API
{
    [Route("api/admin")]
    [EnableCors("AllowSpecificOrigin")]
    public class AdminController : Controller
    {
        private IErrorLogger logger;
        private IComponent<Account> accountComponent;
        private IComponent<Site> siteComponent;

        public AdminController(IComponent<Account> accountComponent, IComponent<Site> siteComponent, IErrorLogger logger)
        {
            this.siteComponent = siteComponent;
            this.accountComponent = accountComponent;
            this.logger = logger;
        }

        [Route("purge/{item}")]
        [HttpPost]
        public string Purge(string item)
        {
            try
            {
                var hash = new StreamReader(HttpContext.Request.Body).ReadToEnd();

                if (hash.Equals(LookupValues.PURGE_HASH))
                {
                    switch (item)
                    {
                        case "account":
                            this.DeleteAccounts();
                            return "Accounts deleted.";
                        case "site":
                            this.DeleteSites();
                            return "Sites deleted.";
                        case "all":
                            this.DeleteAccounts();
                            this.DeleteSites();
                            return "Accounts and sites deleted.";
                        default:
                            return "No action taken.";
                    }
                }
                else 
                {
                    return "You are not authorized.";
                }
            }
            catch (Exception e)
            {
                this.logger.Write("Purge", e.Message);
                return e.Message;
            }
        }

        private void DeleteAccounts()
        {
            var accounts = this.accountComponent.Get();

            if (accounts.Count() > 0)
            {
                try
                {
                    foreach (var account in accounts)
                    {
                        this.accountComponent.Delete(ObjectId.Parse(account.Id));
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new Exception("No accounts exist.");
            }
        }

        private void DeleteSites()
        {
            var sites = this.siteComponent.Get();

            if (sites.Count() > 0)
            {
                try
                {
                    foreach (var site in sites)
                    {
                        this.siteComponent.Delete(ObjectId.Parse(site.Id));
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new Exception("No sites exist.");
            }
        }
    }
}