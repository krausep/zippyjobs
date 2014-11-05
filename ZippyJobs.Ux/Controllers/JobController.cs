using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZippyJobs.Ux.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int jobId)
        {
            ViewBag.JobId = jobId;
            ViewBag.ApiUrl = "http://localhost:63942/api/job/" + jobId;

            return View();
        }
    }
}