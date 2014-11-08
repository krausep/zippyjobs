using System.Linq;
using System.Web.Http;
using Couchbase;
using Couchbase.Extensions;
using ZippyJobs.Models;

namespace ZippyJobs.Web.Controllers.Api
{
    public class RewardController : ApiController
    {
        private static readonly CouchbaseClient Client = new CouchbaseClient();

        [HttpGet]
        [Route("api/reward")]
        public IHttpActionResult Reward()
        {
            var view = Client.GetView("child", "rewards");
            var results = view.Select(reward => Client.GetJson<Reward>(reward.ItemId)).ToList();

            return Ok(results);
        }

        [HttpGet]
        [Route("api/reward/{rewardId}")]
        public IHttpActionResult Reward(int rewardId)
        {
            var c = new Reward { RewardId = rewardId };
            var reward = Client.GetJson<Reward>(c.Key);

            if (reward == null)
                return NotFound();

            return Ok(reward);
        }
    }
}
