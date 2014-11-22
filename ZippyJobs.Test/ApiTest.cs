using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.Results;
using ZippyJobs.Web.Controllers.Api;
using ZippyJobs.Models;
using System;
using Couchbase;
using Couchbase.Extensions;
using Enyim.Caching.Memcached;

namespace ZippyJobs.Test
{
    [TestClass]
    public class ApiTest
    {
        //[TestMethod]
        //public async Task ChildControllerReturnsChildren()
        //{
        //    var children = await GetChild();
        //    Assert.IsNotNull(children);
        //    Assert.AreEqual(2, children.Count);
        //}
        //public static async Task<List<Child>> GetChild()
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var response = httpClient.GetAsync(new Uri("http://localhost:63942/api/child")).Result;
        //    response.EnsureSuccessStatusCode();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsAsync<List<Child>>();
        //    }

        //    return new List<Child>();
        //}

        [TestMethod]
        public void JobController_ReturnsData()
        {
            var controller = new JobController();
            var httpActionResult = controller.Job();
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<List<Job>>));
            var helpMe = (OkNegotiatedContentResult<List<Job>>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreNotEqual(0, helpMe.Content.Count);
            foreach(var result in helpMe.Content)
            {
                Assert.AreEqual("job", result.Type);
            }
        }

        [TestMethod]
        public void JobController_ReturnsSingleRecord()
        {
            var controller = new JobController();
            var httpActionResult = controller.Job(1);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Job>));
            var helpMe = (OkNegotiatedContentResult<Job>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreEqual("job", helpMe.Content.Type);
            Assert.AreEqual(1, helpMe.Content.JobId);
        }

        [TestMethod]
        public void ChildController_ReturnsData()
        {
            var controller = new ChildController();
            var httpActionResult = controller.Child();
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<List<Child>>));
            var helpMe = (OkNegotiatedContentResult<List<Child>>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreNotEqual(0, helpMe.Content.Count);
            foreach (var result in helpMe.Content)
            {
                Assert.AreEqual("child", result.Type);
            }
        }

        [TestMethod]
        public void ChildController_ReturnsSingleRecord()
        {
            var controller = new ChildController();
            var httpActionResult = controller.Child(1);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Child>));
            var helpMe = (OkNegotiatedContentResult<Child>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreEqual("child", helpMe.Content.Type);
            Assert.AreEqual(1, helpMe.Content.ChildId);
        }

        [TestMethod]
        public void RewardController_ReturnsData()
        {
            var controller = new RewardController();
            var httpActionResult = controller.Reward();
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<List<Reward>>));
            var helpMe = (OkNegotiatedContentResult<List<Reward>>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreNotEqual(0, helpMe.Content.Count);
            foreach (var result in helpMe.Content)
            {
                Assert.AreEqual("reward", result.Type);
            }
        }

        [TestMethod]
        public void RewardController_ReturnsSingleRecord()
        {
            var controller = new RewardController();
            var httpActionResult = controller.Reward(1);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Reward>));
            var helpMe = (OkNegotiatedContentResult<Reward>)httpActionResult;
            Assert.AreNotEqual(null, helpMe.Content);
            Assert.AreEqual("reward", helpMe.Content.Type);
            Assert.AreEqual(1, helpMe.Content.RewardId);
        }

        [TestMethod]
        public void ChildPostCreatesNewRecord()
        {
            var controller = new ChildController();
            var newChild = new Child { Name = "New Child", Birthday = DateTime.Now.Date, Jobs = new List<int> { 1, 3, 7, 9 } };


            var httpActionResult = controller.Post(newChild);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Child>));
            var result = (OkNegotiatedContentResult<Child>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("child", result.Content.Type);

            var client = new CouchbaseClient();
            var currentCounter = client.Get("childid::count");
            Assert.AreEqual(Convert.ToInt32(currentCounter), result.Content.ChildId);

            // clean up
            client.Remove(newChild.Key);
        }

        [TestMethod]
        public void ChildPutUpdatesRecord()
        {
            var client = new CouchbaseClient();
            var child = client.GetJson<Child>("child_1");

            var controller = new ChildController();

            var oldName = child.Name;
            child.Name = "UpdChild";

            var oldCounter = client.Get("childid::count");

            var httpActionResult = controller.Put(child);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Child>));
            var result = (OkNegotiatedContentResult<Child>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("child", result.Content.Type);

            var currentCounter = client.Get("childid::count");

            // make sure they didn't change on an update
            Assert.AreEqual(oldCounter, currentCounter);
            // make sure the ChildId didn't increment
            Assert.AreEqual(child.ChildId, result.Content.ChildId);

            // clean up
            child.Name = oldName;
            client.StoreJson(StoreMode.Set, child.Key, child);
        }

        [TestMethod]
        public void JobPostCreatesNewRecord()
        {
            var controller = new JobController();
            var newJob = new Job { Description = "New Job", PointValue = 10 };


            var httpActionResult = controller.Post(newJob);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Job>));
            var result = (OkNegotiatedContentResult<Job>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("job", result.Content.Type);

            var client = new CouchbaseClient();
            var currentCounter = client.Get("jobid::count");
            Assert.AreEqual(Convert.ToInt32(currentCounter), result.Content.JobId);

            // clean up
            client.Remove(newJob.Key);
        }

        [TestMethod]
        public void JobPutUpdatesRecord()
        {
            var client = new CouchbaseClient();
            var job = client.GetJson<Job>("job_1");

            var controller = new JobController();

            var oldDesc = job.Description;
            job.Description = "UpdJob";

            var oldCounter = client.Get("jobid::count");

            var httpActionResult = controller.Put(job);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Job>));
            var result = (OkNegotiatedContentResult<Job>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("job", result.Content.Type);

            var currentCounter = client.Get("jobid::count");

            // make sure they didn't change on an update
            Assert.AreEqual(oldCounter, currentCounter);
            // make sure the ChildId didn't increment
            Assert.AreEqual(job.JobId, result.Content.JobId);

            // clean up
            job.Description = oldDesc;
            client.StoreJson(StoreMode.Set, job.Key, job);
        }

        [TestMethod]
        public void RewardPostCreatesNewRecord()
        {
            var controller = new RewardController();
            var newReward = new Reward { Description = "New Reward", PointValue = 100 };


            var httpActionResult = controller.Post(newReward);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Reward>));
            var result = (OkNegotiatedContentResult<Reward>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("reward", result.Content.Type);

            var client = new CouchbaseClient();
            var currentCounter = client.Get("rewardid::count");
            Assert.AreEqual(Convert.ToInt32(currentCounter), result.Content.RewardId);

            // clean up
            client.Remove(newReward.Key);
        }

        [TestMethod]
        public void RewardPutUpdatesRecord()
        {
            var client = new CouchbaseClient();
            var reward = client.GetJson<Reward>("reward_1");

            var controller = new RewardController();

            var oldDesc = reward.Description;
            reward.Description = "UpdReward";

            var oldCounter = client.Get("rewardid::count");

            var httpActionResult = controller.Put(reward);
            Assert.AreNotEqual(null, httpActionResult);
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<Reward>));
            var result = (OkNegotiatedContentResult<Reward>)httpActionResult;
            Assert.AreNotEqual(null, result.Content);
            Assert.AreEqual("reward", result.Content.Type);

            var currentCounter = client.Get("rewardid::count");

            // make sure they didn't change on an update
            Assert.AreEqual(oldCounter, currentCounter);
            // make sure the ChildId didn't increment
            Assert.AreEqual(reward.RewardId, result.Content.RewardId);

            // clean up
            reward.Description = oldDesc;
            client.StoreJson(StoreMode.Set, reward.Key, reward);
        }
    }


}
