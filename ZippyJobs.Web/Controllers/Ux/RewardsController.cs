using System.Web.Mvc;

namespace ZippyJobs.Web.Controllers.Ux
{
    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult Index()
        {
            ViewBag.ApiUrl = "http://localhost:3334/api/reward";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.JobId = id;
            ViewBag.ApiUrl = "http://localhost:3334/api/reward/" + id;

            return View();
        }
    }
}