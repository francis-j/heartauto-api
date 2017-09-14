using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using BE.AccountEntities;
using BLL;
using DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace API
{
    [Route("api/account")]
    [EnableCors("AllowSpecificOrigin")]
    public class AccountController : BaseController<Account>
    {
        public AccountController(IComponent<Account> component, IErrorLogger logger) : base(component, logger)
        {

        }

        [Route("login")]
        [HttpPost("account")]
        public IActionResult Login([FromBody]AccountLogin account)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    var result = (this.component as AccountComponent).Login(account);
                    result.Password = string.Empty;

                    return Ok(result);
                }
                catch (Exception e) 
                {
                    this.logger.Write("Login", e.Message);
                    return BadRequest("Login failed. Please try again.");
                }
            }
            else 
            {
                var error = "Invalid login request";
                this.logger.Write("Register", error);
                return BadRequest(error);
            }
        }

        [Route("register")]
        [HttpPost("account")]
        public IActionResult Register([FromBody]Account account)
        {
            if (ModelState.IsValid)
            {
                var filters = new List<KeyValuePair<string, object>>();
                filters.Add(new KeyValuePair<string, object>("Email", account.Email));

                try 
                {
                    List<Account> accounts = this.component.Get(filters).ToList();

                    if (accounts.Count > 0)
                    {
                        var error = "This email address has already been registered.";
                        this.logger.Write("Registration", error);
                        return BadRequest(error);
                    }
                    else
                    {
                        account.Id = ObjectId.GenerateNewId().ToString();
                        this.component.Add(account);

                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    this.logger.Write(e.Source, e.Message);
                    return BadRequest(e.Message);
                }     
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(x => x.Errors);

                var errorMessage = "Registration failed:<ul>";
                
                foreach (var error in errors)
                {
                    errorMessage += "<li>";
                    errorMessage += error.ErrorMessage.ToString();
                    errorMessage += "</li>";
                }

                errorMessage += "</ul>";

                this.logger.Write("Registration", errorMessage);
                return BadRequest(errorMessage);
            }                   
        }
    }
}