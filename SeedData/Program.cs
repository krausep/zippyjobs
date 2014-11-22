using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Extensions;
using Enyim.Caching.Memcached;
using ZippyJobs.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SeedZippyJobsAndRewards
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoveChildren();
            RemoveRewards();
            RemoveJobs();
            ResetCounters();
            AddJobs();
            AddRewards();
            AddChildren();

            //var t = new Task(GetChild);
            //t.Start();
            //t.Wait();
        }

        private static void ResetCounters()
        {
            Console.WriteLine("Resetting counters");
            var client = new CouchbaseClient();
            client.Remove("jobid::count");
            client.Remove("childid::count");
            client.Remove("rewardid::count");
        }

        private static void AddChildren()
        {
            Console.WriteLine("Creating children");
            var client = new CouchbaseClient();
            var children = new List<Child>
            {
                new Child{ChildId = Convert.ToInt32(GetNextChildId()), Name = "David", Birthday = DateTime.Parse("09/30/2006 00:00:00"), Jobs = new List<int> {1,3,4,5,6,7,8,9,10}},
                new Child{ChildId = Convert.ToInt32(GetNextChildId()), Name = "Nathan", Birthday = DateTime.Parse("12/19/2008 00:00:00"), Jobs = new List<int> {2,4,6,8,10}},
                new Child{ChildId = Convert.ToInt32(GetNextChildId()), Name = "Susie", Birthday = DateTime.Parse("3/16/2010 00:00:00"), Jobs = new List<int> {1,3,5,7,9,11}},
            };
            foreach (var child in children)
                client.StoreJson(StoreMode.Set, child.Key, child);
        }

        private static ulong GetNextJobId()
        {
            var client = new CouchbaseClient();
            return client.Increment("jobid::count", 1UL, 1UL);
        }

        private static ulong GetNextChildId()
        {
            var client = new CouchbaseClient();
            return client.Increment("childid::count", 1UL, 1UL);
        }

        private static ulong GetNextRewardId()
        {
            var client = new CouchbaseClient();
            return client.Increment("rewardid::count", 1UL, 1UL);
        }

        private static void RemoveChildren()
        {
            Console.WriteLine("Removing children");
            var client = new CouchbaseClient();

            var view = client.GetView("child", "children");
            var results = view.Select(child => client.GetJson<Child>(child.ItemId)).ToList();

            foreach(var child in results)
                client.Remove(child.Key);
        }

        private static void AddJobs()
        {
            Console.WriteLine("Adding jobs");
            var client = new CouchbaseClient();
            var jobs = new List<Job>
                {
                    new Job{Description = "Table manners", PointValue = 5},
                    new Job{Description = "Clear table", PointValue = 10},
                    new Job{Description = "Public behavior", PointValue = 5},
                    new Job{Description = "Put away dishes", PointValue = 5},
                    new Job{Description = "Brush teeth", PointValue = 5},
                    new Job{Description = "Put on PJs", PointValue = 5},
                    new Job{Description = "Put away dirty clothes", PointValue = 5},
                    new Job{Description = "Clean bedroom", PointValue = 20},
                    new Job{Description = "20 minutes of reading", PointValue = 20},
                    new Job{Description = "Help with cleaning", PointValue = 20},
                    new Job{Description = "Good listening", PointValue = 20},


                };
            foreach (var job in jobs)
            {
                job.JobId = Convert.ToInt32(GetNextJobId());
                client.StoreJson(StoreMode.Add, job.Key, job);
            }
        }

        private static void AddRewards()
        {
            Console.WriteLine("Adding rewards");
            var client = new CouchbaseClient();
            var rewards = new List<Reward>
             {
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Video games (20 minutes)", PointValue = 50, RedeemableInterval = RedeemableInterval.TwiceDaily},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Video games (60 minutes)", PointValue = 250, RedeemableInterval = RedeemableInterval.OnceDaily},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Bedtime story", PointValue = 50, RedeemableInterval = RedeemableInterval.Unlimited},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Tickle time", PointValue = 100, RedeemableInterval = RedeemableInterval.Unlimited},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Family game night", PointValue = 150, RedeemableInterval = RedeemableInterval.Unlimited},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "Go to the park", PointValue = 200, RedeemableInterval = RedeemableInterval.Unlimited},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "TV Time (20 minutes)", PointValue = 50, RedeemableInterval = RedeemableInterval.Unlimited},
                 new Reward{RewardId = Convert.ToInt32(GetNextRewardId()), Description = "TV Time (60 minutes)", PointValue = 250, RedeemableInterval = RedeemableInterval.Unlimited},

             };
            foreach (var reward in rewards)
            {
                client.StoreJson(StoreMode.Add, reward.Key, reward);
            }
        }

        private static void RemoveJobs()
        {
            Console.WriteLine("Removing jobs");
            var client = new CouchbaseClient();
            var view = client.GetView("child", "jobs");
            var results = view.Select(job => client.GetJson<Job>(job.ItemId)).ToList();

            foreach (var job in results)
                client.Remove(job.Key);

        }

        private static void RemoveRewards()
        {
            Console.WriteLine("Removing rewards");
            var client = new CouchbaseClient();
            var view = client.GetView("child", "rewards");
            var results = view.Select(reward => client.GetJson<Reward>(reward.ItemId)).ToList();

            foreach (var reward in results)
                client.Remove(reward.Key);

        }

        public static async void GetChild()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync(new Uri("http://localhost:63942/child/children")).Result;
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                var thing = await response.Content.ReadAsAsync<List<Child>>();
                Console.WriteLine(thing);
            }
        }
    }
}
