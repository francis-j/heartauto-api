using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using BE;
using BLL;
using DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace API
{
    [Route("api/site")]
    [EnableCors("AllowSpecificOrigin")]
    public class SiteController : BaseController<Site>
    {
        public SiteController(IComponent<Site> component, IErrorLogger logger) : base(component, logger)
        {
            
        }

        [HttpGet]
        [Route("latest/{count}")]
        public IActionResult Latest(int count) 
        {
            try 
            {
                var result = (this.component as SiteComponent).GetLatest(count);
                foreach (var site in result) 
                {
                    site.AccessKey = Guid.Empty;
                }

                return Ok(result);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("my/{accountId}")]
        public IActionResult My(string accountId) 
        {
            try 
            {
                var site = new Site();
                var filters = new List<KeyValuePair<string, object>>() 
                {
                    new KeyValuePair<string, object>(nameof(site.AccountId), accountId)
                };

                var result = (this.component as SiteComponent).Get(filters);

                return Ok(result);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("validateAccessKey")]
        public IActionResult ValidateAccessKey([FromBody]string[] data) 
        {
            try 
            {
                var id = ObjectId.Parse(data[0]);
                var key = data[1];

                var site = this.component.GetById(id) as Site;
                
                if (Guid.Parse(key) == site.AccessKey)
                    return Ok();
                else
                    throw new Exception("Access key incorrect");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}