using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZippyJobs.Ux.Controllers
{
    public class ChildrenController : Controller
    {
        // GET: Children
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:63942/api/child";
            return View();
        }
    }
}