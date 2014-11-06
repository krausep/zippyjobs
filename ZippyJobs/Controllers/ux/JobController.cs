using System.Web.Mvc;

namespace ZippyJobs.Controllers.ux
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