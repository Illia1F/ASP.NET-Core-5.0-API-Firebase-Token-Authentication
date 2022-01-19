namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseTokenController : ControllerBase
    {
        // GET: api/<FirebaseTokenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FirebaseTokenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FirebaseTokenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FirebaseTokenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FirebaseTokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
