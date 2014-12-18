using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhimHang.Controllers
{

    public class CommentController : ApiController
    {
        private testEntities db = new testEntities();
        // GET api/comment
        public dynamic Get()
        {
            var ret = (from c in db.PostComments.ToList()
                       orderby c.PostedDate descending
                       select new
                       {

                       });
            return ret;

        }

        // GET api/comment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/comment
        public void Post([FromBody]string value)
        {

        }

        // PUT api/comment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/comment/5
        public void Delete(int id)
        {
        }
    }
}
