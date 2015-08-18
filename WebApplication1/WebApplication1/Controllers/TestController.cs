using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class TestController : ApiController
    {
        //// GET: api/Controller
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Controller/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Controller
        [HttpPost,AcceptVerbs("POST")]
        [Route("MyPost")]
        public string Post([FromBody]string value)
        {
            return "posted";
        }

        //// PUT: api/Controller/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Controller/5
        //public void Delete(int id)
        //{
        //}
    }
}
