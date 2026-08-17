using QA_Hybrid_Framework.API;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QA_Hybrid_Framework.Tests
{
    [TestFixture]
    [Category("API")]
    public class ApiTests
    {
        private readonly ReqResClient _apiClient = new ReqResClient();

        [Test]
        [Category("Smoke")]
        public void GetUsersList_ShouldReturn200OK()
        {
            var response = _apiClient.GetUsers();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response code is not 200");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Category("Regression")]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            var response = _apiClient.GetUserById(1);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Does.Contain("Leanne Graham"));
        }

        [Test]
        [Category("Smoke")]
        public void CreateUser_ShouldReturn201Created()
        {
            var response = _apiClient.CreateUser("NewUser123", "Engineer");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Response code is not 201");
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Name, Is.EqualTo("NewUser123"), "User name does not match!");
        }

        [Test]
        [Category("Regression")]
        public void UpdateUser_ShouldReturn200OK()
        {
            var response = _apiClient.UpdateUser(1, "UpdatedName", "SeniorQA");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Does.Contain("SeniorQA"));
        }

        [Test]
        [Category("Regression")]
        public void DeleteUser_ShouldReturn200OK()
        {
            var response = _apiClient.DeleteUser(1);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [Category("Negative")]
        public void GetNonExistingUser_ShouldReturn404NotFound()
        {
            var response = _apiClient.GetNonExistingUser();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
