using System.Web.Mvc;

namespace ZippyJobs.Web.Controllers.Ux
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:3334/api/job";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.JobId = id;
            ViewBag.ApiUrl = "http://localhost:3334/api/job/" + id;

            return View();
        }
    }
}