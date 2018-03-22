using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataService.Controllers
{
    public class MeetingRoomController : ApiController
    {
        // GET: api/MeetingRoom
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MeetingRoom/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MeetingRoom
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MeetingRoom/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MeetingRoom/5
        public void Delete(int id)
        {
        }
    }
}
