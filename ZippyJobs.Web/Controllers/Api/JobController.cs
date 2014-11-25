using System.Linq;
using System.Web.Http;
using Couchbase;
using Couchbase.Extensions;
using ZippyJobs.Models;
using Enyim.Caching.Memcached;
using System;

namespace ZippyJobs.Web.Controllers.Api
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

        [HttpPost]
        [Route("api/job")]
        public IHttpActionResult Post([FromBody] Job job)
        {
            // Create a new job
            var newId = Client.Increment("jobid::count", 1UL, 1UL);

            job.JobId = Convert.ToInt32(newId);

            Client.StoreJson(StoreMode.Set, job.Key, job);

            var view = Client.GetView("child", "jobs").Stale(StaleMode.False);
            var results = view.Select(j => Client.GetJson<Job>(j.ItemId)).ToList();

            return Ok(job);
        }

        [HttpPut]
        [Route("api/job")]
        public IHttpActionResult Put([FromBody] Job job)
        {
            // Update a record
            if (job == null || job.JobId == 0) return NotFound();

            Client.StoreJson(StoreMode.Add, job.Key, job);

            return Ok(job);
        }
    }
}
