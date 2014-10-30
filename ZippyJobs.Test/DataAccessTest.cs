using System;
using System.Runtime.InteropServices;
using Couchbase.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Couchbase;
using Enyim.Caching.Memcached;
using ZippyJobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ZippyTest
{
    [TestClass]
    public class DataAccessTest
    {
        [TestInitialize]
        public void InitializeTest()
        {
            
        }

        [TestMethod]
        public void StoreStringWorks()
        {
            const string testKey = "testKey";
            const string testValue = "testValue";

            // test setup
            var client = new CouchbaseClient();
            client.Remove(testKey);

            client.Store(StoreMode.Add, testKey, testValue);
            var someValue = client.Get(testKey) as string;
            Assert.AreEqual(someValue, testValue);

        }

        [TestMethod]
        public void StoreJobWorks()
        {
            var testJob = new Job() {JobId = 1, Description = "Job 1", PointValue = 5};

            var client = new CouchbaseClient();
            client.Remove("job_" + testJob.JobId);

            client.Store(StoreMode.Add, "job_" + testJob.JobId, testJob);
            var job = client.Get<Job>("job_" + testJob.JobId);
            Assert.AreEqual(testJob.JobId, job.JobId);
            Assert.AreEqual(testJob.Description, job.Description);
            Assert.AreEqual(testJob.PointValue, job.PointValue);
        }

        [TestMethod]
        public void StoreRewardWorks()
        {
            var testReward = new Reward() { RewardId = 1, Description = "Reward 1", PointValue = 5, RedeemableInterval = RedeemableInterval.OnceDaily };

            var client = new CouchbaseClient();
            client.Remove(testReward.Key);

            client.Store(StoreMode.Add, testReward.Key, testReward);
            var reward = client.Get<Reward>(testReward.Key);
            Assert.AreEqual(testReward.RewardId, reward.RewardId);
            Assert.AreEqual(testReward.Description, reward.Description);
            Assert.AreEqual(testReward.PointValue, reward.PointValue);
            Assert.AreEqual(testReward.RedeemableInterval, reward.RedeemableInterval);
        }

        [TestMethod]
        public void StoreAndGetJobJsonWorks()
        {
            var testJob = new Job() { JobId = 1, Description = "Job 1", PointValue = 5 };

            var client = new CouchbaseClient();
            client.Remove(testJob.Key);

            client.StoreJson(StoreMode.Add, testJob.Key, testJob);
            var job = client.GetJson<Job>(testJob.Key);
            Assert.AreEqual(testJob.JobId, job.JobId);
            Assert.AreEqual(testJob.Description, job.Description);
            Assert.AreEqual(testJob.PointValue, job.PointValue);
        }

        [TestMethod]
        public void StoreAndSetJobJsonWorks()
        {
            var testJob = new Job() { JobId = 1, Description = "Job 1", PointValue = 5 };

            var client = new CouchbaseClient();
            client.Remove(testJob.Key);

            client.StoreJson(StoreMode.Add, testJob.Key, testJob);
            var job = client.GetJson<Job>(testJob.Key);
            Assert.AreEqual(testJob.JobId, job.JobId);
            Assert.AreEqual(testJob.Description, job.Description);
            Assert.AreEqual(testJob.PointValue, job.PointValue);

            // update a value to see if the Set operation works
            testJob.Description = "you have been updated";
            client.StoreJson(StoreMode.Set, testJob.Key, testJob);
            job = client.GetJson<Job>(testJob.Key);
            Assert.AreEqual(testJob.JobId, job.JobId);
            Assert.AreEqual(testJob.Description, job.Description);
            Assert.AreEqual(testJob.PointValue, job.PointValue);
        }

        [TestMethod]
        public void StoreAndGetRewardWorks()
        {
            var testReward = new Reward() { RewardId = 1, Description = "Reward 1", PointValue = 5 };

            var client = new CouchbaseClient();
            client.Remove(testReward.Key);

            client.StoreJson(StoreMode.Add, testReward.Key, testReward);
            var reward = client.GetJson<Reward>(testReward.Key);
            Assert.AreEqual(testReward.RewardId, reward.RewardId);
            Assert.AreEqual(testReward.Description, reward.Description);
            Assert.AreEqual(testReward.PointValue, reward.PointValue);
            Assert.AreEqual(testReward.RedeemableInterval, reward.RedeemableInterval);
        }

        [TestMethod]
        public void StoreAndSetRewardWorks()
        {
            var testReward = new Reward() { RewardId = 1, Description = "Reward 1", PointValue = 5 };

            var client = new CouchbaseClient();
            client.Remove(testReward.Key);

            client.StoreJson(StoreMode.Add, testReward.Key, testReward);
            var reward = client.GetJson<Reward>(testReward.Key);
            Assert.AreEqual(testReward.RewardId, reward.RewardId);
            Assert.AreEqual(testReward.Description, reward.Description);
            Assert.AreEqual(testReward.PointValue, reward.PointValue);
            Assert.AreEqual(testReward.RedeemableInterval, reward.RedeemableInterval);

            // update a property to try to use the Set operation
            testReward.Description = "I just updated you";
            client.StoreJson(StoreMode.Set, testReward.Key, testReward);
            reward = client.GetJson<Reward>(testReward.Key);

            Assert.AreEqual(testReward.RewardId, reward.RewardId);
            Assert.AreEqual(testReward.Description, reward.Description);
            Assert.AreEqual(testReward.PointValue, reward.PointValue);
            Assert.AreEqual(testReward.RedeemableInterval, reward.RedeemableInterval);
        }

        [TestMethod]
        public void StoreChildWorks()
        {
            var testChild = new Child() { ChildId = 1, Name = "David", Birthday = DateTime.Parse("09/30/2008 00:00:00") };

            var client = new CouchbaseClient();
            client.Remove(testChild.Key);

            client.Store(StoreMode.Add, testChild.Key, testChild);
            var child = client.Get<Child>(testChild.Key);

            Assert.AreEqual(testChild.ChildId, child.ChildId);
            Assert.AreEqual(testChild.Name, child.Name);
            Assert.AreEqual(testChild.Birthday, child.Birthday);
        }

        [TestMethod]
        public void StoreAndSetChildJsonWorks()
        {
            var testChild = new Child() { ChildId = 1, Name = "David", Birthday = DateTime.Parse("09/30/2008 00:00:00") };

            var client = new CouchbaseClient();
            client.Remove(testChild.Key);

            client.StoreJson(StoreMode.Add, testChild.Key, testChild);
            var child = client.GetJson<Child>(testChild.Key);

            Assert.AreEqual(testChild.ChildId, child.ChildId);
            Assert.AreEqual(testChild.Name, child.Name);
            Assert.AreEqual(testChild.Birthday, child.Birthday);

            testChild.Name = testChild.Name + "_1";
            client.StoreJson(StoreMode.Set, testChild.Key, testChild);
            child = client.GetJson<Child>(testChild.Key);

            Assert.AreEqual(testChild.ChildId, child.ChildId);
            Assert.AreEqual(testChild.Name, child.Name);
            Assert.AreEqual(testChild.Birthday, child.Birthday);
        }

        [TestMethod]
        public void JobViewReturnsData()
        {
            var client = new CouchbaseClient();

            //var view = client.GetView("beer", "by_name");
            var view = client.GetView("child", "jobs");

            // TODO: need to figure out how to parse the results, but resharper
            // can't verify and my intellisense isn't working and it's pissing me off
            foreach (var row in view)
            {
                var doc = client.GetJson<Job>(row.ItemId);
                Assert.IsNotNull(doc.Description);
            }
        }

        [TestMethod]
        public void RewardViewReturnsData()
        {
            var client = new CouchbaseClient();

            //var view = client.GetView("beer", "by_name");
            var view = client.GetView("child", "rewards");

            foreach (var row in view)
            {
                var doc = client.GetJson<Reward>(row.ItemId);
                Assert.IsNotNull(doc.Description);
            }
        }

        [TestMethod]
        public void ChildViewReturnsData()
        {
            var client = new CouchbaseClient();

            //var view = client.GetView("beer", "by_name");
            var view = client.GetView("child", "children");

            // TODO: need to figure out how to parse the results, but resharper
            // can't verify and my intellisense isn't working and it's pissing me off
            foreach (var row in view)
            {
                var doc = client.GetJson<Child>(row.ItemId);
                Assert.IsNotNull(doc.Name);
            }
        }

    }

}
