using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Couchbase;
using Couchbase.Extensions;
using ZippyJobs.Models;
using System.Web;

namespace ZippyJobs.Controllers
{
    public class ChildController : ApiController
    {
        private static readonly CouchbaseClient Client = new CouchbaseClient();
       
        [HttpGet]
        [Route("api/child")]
        public IHttpActionResult Child()
        {
            var view = Client.GetView("child", "children");
            var results = view.Select(child => Client.GetJson<Child>(child.ItemId)).ToList();
            var request = HttpContext.Current.Request;
            var myUrlBase = String.Format("{0}://{1}/api/child/", request.Url.Scheme, request.Url.Authority);
            results.ForEach(c => c.Url = myUrlBase + c.ChildId);

            return Ok(results);
        }

        [HttpGet]
        [Route("api/child/{childId}")]
        public IHttpActionResult Child(int childId)
        {
            var c = new Child { ChildId = childId };
            var child = Client.GetJson<Child>(c.Key);

            if (child == null)
                return NotFound();

            var request = HttpContext.Current.Request;
            var myUrlBase = String.Format("{0}://{1}/api/child/", request.Url.Scheme, request.Url.Authority);
            child.Url = myUrlBase + child.ChildId;
            return Ok(child);
        }
    }
}
