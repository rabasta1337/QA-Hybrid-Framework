using QA_Hybrid_Framework.API.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.API
{
    internal class ReqResClient
    {
        private readonly RestClient _client;

        public ReqResClient()
        {
            var options = new RestClientOptions("https://jsonplaceholder.typicode.com")
            {
                Timeout = TimeSpan.FromMilliseconds(5000)
            };
            _client = new RestClient(options);
        }

        public RestResponse GetUsers()
        {
            var request = new RestRequest("/users/1", Method.Get);
            return _client.Execute(request);
        }

        public RestResponse<CreateUserResponse> CreateUser(string name, string job)
        {
            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(new
            {
                name = name,
                username = job
            });
            return _client.Execute<CreateUserResponse>(request);
        }
    }
}
