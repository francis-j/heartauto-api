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
    }
}