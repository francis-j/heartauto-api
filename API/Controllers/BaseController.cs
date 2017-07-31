using System;
using System.Collections.Generic;
using BLL;
using DAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public abstract class BaseController<T> : Controller
    {
        protected readonly IComponent<T> component;
        protected readonly IErrorLogger logger;

        public BaseController(IComponent<T> component, IErrorLogger logger)
        {
            this.component = component;
            this.logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<T> Get()
        {
            return component.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<T> Get(string id)
		{
            var objectId = new ObjectId(id);

            var filters = new List<KeyValuePair<string, object>>() 
            {
                new KeyValuePair<string, object>("_id", objectId)
            };

            return component.Get(filters);
        }

        // POST api/values
        [HttpPost]
        public virtual bool Post([FromBody]T item)
		{
            try
            {
                component.Add(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }

            return true;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(ObjectId id, [FromBody]T item)
		{
            try
            {
                component.Update(id, item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
                return false;
            }

            return true;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(ObjectId id)
		{
            try
            {
                component.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
                return false;
            }

            return true;
        }
    }
}