using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZippyJobs.Ux.Controllers
{
    public class ChildController : Controller
    {
        // GET: Child
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int childId)
        {
            ViewBag.ChildId = childId;
            ViewBag.ApiUrl = "http://localhost:63942/api/child/" + childId;
            return View();
        }
    }
}