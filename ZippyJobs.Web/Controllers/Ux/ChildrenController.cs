using System.Web.Mvc;

namespace ZippyJobs.Web.Controllers.Ux
{
    public class ChildrenController : Controller
    {
        // GET: Children
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:3334/api/child";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ChildId = id;
            ViewBag.ApiUrl = "http://localhost:3334/api/child/" + id;
            ViewBag.JobUrl = "http://localhost:3334/api/job/";
            ViewBag.ApiBaseUrl = "http://localhost:3334/api/child/";
            return View();
        }
    }
}