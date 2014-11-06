using System.Web.Mvc;

namespace ZippyJobs.Controllers.ux
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:63942/api/job";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.JobId = id;
            ViewBag.ApiUrl = "http://localhost:63942/api/job/" + id;

            return View();
        }
    }
}