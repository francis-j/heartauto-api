using System;
using System.Collections.Generic;
using BLL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public abstract class BaseController<T> : Controller
    {
        public IComponent<T> component;

        // GET: api/values
        [HttpGet]
        public IEnumerable<T> Get()
        {
            return component.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<T> Get(IEnumerable<KeyValuePair<string, object>> filters)
		{
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