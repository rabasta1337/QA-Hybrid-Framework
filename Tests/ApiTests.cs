using QA_Hybrid_Framework.API;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QA_Hybrid_Framework.Tests
{
    public class ApiTests
    {
        private readonly ReqResClient _apiClient = new ReqResClient();

        [Test]
        public void GetUsersList_ShouldReturn200OK()
        {
            var response = _apiClient.GetUsers();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response code is not 200");
        }

        [Test]
        public void CreateUser_ShouldReturn201Created()
        {
            var response = _apiClient.CreateUser("NewUser123", "Engineer");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Response code is not 201");
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Name, Is.EqualTo("NewUser123"), "User name does not match!");
        }
    }
}
