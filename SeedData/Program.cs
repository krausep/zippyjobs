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
            AddJobs();
            AddRewards();
            AddChildren();

            //var t = new Task(GetChild);
            //t.Start();
            //t.Wait();
        }

        private static void AddChildren()
        {
            Console.WriteLine("Creating children");
            var client = new CouchbaseClient();
            var children = new List<Child>
            {
                new Child{ChildId = 1, Name = "David", Birthday = DateTime.Parse("09/30/2006 00:00:00"), Jobs = new List<int> {1,3,4}},
                new Child{ChildId = 2, Name = "Nathan", Birthday = DateTime.Parse("12/18/2008 00:00:00"), Jobs = new List<int> {1,2,3,4,5}},
            };
            foreach (var child in children)
                client.StoreJson(StoreMode.Set, child.Key, child);
        }

        private static void RemoveChildren()
        {
            Console.WriteLine("Removing children");
            var client = new CouchbaseClient();

            for (int i = 0; i <= 2; i++)
                client.Remove("child_" + i);
        }

        private static void AddJobs()
        {
            Console.WriteLine("Adding jobs");
            var client = new CouchbaseClient();
            var jobs = new List<Job>
                {
                    new Job{Description = "Table manners", JobId = 1, PointValue = 5},
                    new Job{Description = "Clean place", JobId = 2, PointValue = 10},
                    new Job{Description = "Public behavior", JobId = 3, PointValue = 5},
                    new Job{Description = "Put away dishes", JobId = 4, PointValue = 5},
                    new Job{Description = "Brush teeth", JobId = 5, PointValue = 20},
                };
            foreach (var job in jobs)
            {
                client.StoreJson(StoreMode.Add, job.Key, job);
            }
        }

        private static void AddRewards()
        {
            Console.WriteLine("Adding rewards");
            var client = new CouchbaseClient();
            var rewards = new List<Reward>
             {
                 new Reward{RewardId = 1, Description = "Video games (20 minutes)", PointValue = 50, RedeemableInterval = RedeemableInterval.TwiceDaily},
                 new Reward{RewardId = 2, Description = "Video games (60 minutes)", PointValue = 250, RedeemableInterval = RedeemableInterval.OnceDaily},
                 new Reward{RewardId = 3, Description = "Read a book", PointValue = 100, RedeemableInterval = RedeemableInterval.Unlimited},
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
            for (int i = 0; i <= 5; i++)
            {
                client.Remove("job_" + i);
            }
        }

        private static void RemoveRewards()
        {
            Console.WriteLine("Removing rewards");
            var client = new CouchbaseClient();
            for (int i = 0; i <= 3; i++)
            {
                client.Remove("reward_" + i);
            }
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
