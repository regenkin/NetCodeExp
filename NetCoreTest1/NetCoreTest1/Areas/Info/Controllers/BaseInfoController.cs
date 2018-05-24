using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace netcoretest.Areas.Info.Controllers
{
    [Produces("application/json")]
    [Route("api/Info/BaseInfo")]
    public class BaseInfoController : Controller
    {
        // GET: api/BaseInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "BaseInfo1", "BaseInfo2" };
        }

        //// GET: api/BaseInfo/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/BaseInfo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/BaseInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
