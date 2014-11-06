using System.Web.Mvc;

namespace ZippyJobs.Controllers.ux
{
    public class ChildrenController : Controller
    {
        // GET: Children
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:63942/api/child";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ChildId = id;
            ViewBag.ApiUrl = "http://localhost:63942/api/child/" + id;
            ViewBag.JobUrl = "http://localhost:63942/api/job/";
            return View();
        }
    }
}