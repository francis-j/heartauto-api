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
        public virtual IActionResult Get()
        {
            try 
            {
                var result = component.Get();

                return Ok(result);
            }
            catch (Exception e) 
            {
                logger.Write(e.Source, e.Message);
                return BadRequest(e.Message);
            }            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(string id)
		{
            try 
            {
                var objectId = new ObjectId(id);

                var filters = new List<KeyValuePair<string, object>>() 
                {
                    new KeyValuePair<string, object>("_id", objectId)
                };

                var result = component.Get(filters);

                return Ok(result);
            }
            catch (Exception e) 
            {
                logger.Write(e.Source, e.Message);
                return BadRequest(e.Message);
            }      
        }

        // POST api/values
        [HttpPost]
        public virtual IActionResult Post([FromBody]T item)
		{
            try
            {
                var result = component.Add(item);

                return Ok(result);
            }
            catch (Exception e) 
            {
                logger.Write(e.Source, e.Message);
                return BadRequest(e.Message);
            }      
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual IActionResult Put(ObjectId id, [FromBody]T item)
		{
            try
            {
                var result = component.Update(id, item);

                return Ok(result);
            }
            catch (Exception e) 
            {
                logger.Write(e.Source, e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(ObjectId id)
		{
            try
            {
                var result = component.Delete(id);
                
                return Ok(result);
            }
            catch (Exception e) 
            {
                logger.Write(e.Source, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}