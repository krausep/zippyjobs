using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZippyJobs.Models;

namespace ZippyJobs.Test
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public async Task ChildControllerReturnsChildren()
        {
            var children = await GetChild();
            Assert.IsNotNull(children);
            Assert.AreEqual(2, children.Count);
        }
        public static async Task<List<Child>> GetChild()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync(new Uri("http://localhost:63942/api/child")).Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Child>>();
            }

            return new List<Child>();
        }
    }


}
