using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using BE;
using BLL;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API
{
    public class AccountController : BaseController<Account>
    {
        public AccountController(IComponent<Account> component)
        {
            base.component = component;
        }

        [HttpGet("username, password")]
        public IActionResult Login([FromBody]Account account)
        {
            if ((this.component as AccountComponent).Login(account) != null) 
            {
                if (ModelState.IsValid)
                {
                    var result = (base.component as AccountComponent).Login(account);

                    if (result == null)
                        return BadRequest("Login failed");
                }
                else 
                {
                    return BadRequest("Invalid login request");
                }
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Register([FromBody]Account account)
        {
            var filters = new List<KeyValuePair<string, object>>();
            filters.Add(new KeyValuePair<string, object>("email", account.Email));

            if ((this.component as AccountComponent).Get(filters) == null)
            {
                account.Id = ObjectId.GenerateNewId().ToString();
                (this.component as AccountComponent).Add(account);

                return Ok();
            }
            else
            {
                return BadRequest("Login already exists");
            }
        }
    }
}