﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Couchbase;
using Couchbase.Extensions;
using ZippyJobs.Models;

namespace ZippyJobs.Controllers
{
    public class ChildController : ApiController
    {
        private static readonly CouchbaseClient Client = new CouchbaseClient();
       
        [HttpGet]
        public IHttpActionResult Children()
        {
            var view = Client.GetView("child", "children");
            var results = view.Select(child => Client.GetJson<Child>(child.ItemId)).ToList();

            return Ok(results);
        }

        [HttpGet]
        [Route("child/{childId}")]
        public IHttpActionResult Child(int childId)
        {
            var child = Client.GetJson<Child>(String.Format("child_{0}", childId));

            if (child == null)
                return NotFound();

            return Ok(child);
        }
    }
}
