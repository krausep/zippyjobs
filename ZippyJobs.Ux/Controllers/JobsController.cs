using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZippyJobs.Ux.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:63942/api/job";
            return View();
        }
    }
}