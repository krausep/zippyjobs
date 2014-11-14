using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.Results;
using ZippyJobs.Web.Controllers.Api;
using ZippyJobs.Models;

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
    }


}
