using Couchbase;
using Couchbase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZippyJobs.Models;

namespace ZippyJobs.Controllers
{
    public class JobController : ApiController
    {
        private static readonly CouchbaseClient Client = new CouchbaseClient();

        [HttpGet]
        [Route("api/job")]
        public IHttpActionResult Job()
        {
            var view = Client.GetView("child", "jobs");
            var results = view.Select(job => Client.GetJson<Job>(job.ItemId)).ToList();

            return Ok(results);
        }

        [HttpGet]
        [Route("api/job/{jobId}")]
        public IHttpActionResult Job(int jobId)
        {
            var c = new Job { JobId = jobId };
            var job = Client.GetJson<Job>(c.Key);

            if (job == null)
                return NotFound();

            return Ok(job);
        }
    }
}
