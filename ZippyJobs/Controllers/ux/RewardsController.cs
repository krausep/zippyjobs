using System.Web.Mvc;

namespace ZippyJobs.Controllers.ux
{
    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:63942/api/reward";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.JobId = id;
            ViewBag.ApiUrl = "http://localhost:63942/api/reward/" + id;

            return View();
        }
    }
}