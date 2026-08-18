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

        // Inicjalizacja klienta bazowego z konfiguracją adresu URL bazowego i timeoutu dla odpowiedzi
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

        public RestResponse GetUserById(int id)
        {
            var request = new RestRequest($"users/{id}", Method.Get);
            return _client.Execute(request);
        }

        // Utworzenie nowego użytkownika z automatyczną deserializacją odpowiedzi do DTO
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

        public RestResponse<CreateUserResponse> UpdateUser(int id, string updatedName, string updatedJob)
        {
            var request = new RestRequest($"/users/{id}", Method.Put);
            request.AddJsonBody(new
            {
                name = updatedName,
                job = updatedJob
            });
            return _client.Execute<CreateUserResponse>(request);
        }


        public RestResponse DeleteUser(int id)
        {
            var request = new RestRequest($"/users/{id}", Method.Delete);
            return _client.Execute(request);

        }

        //Próba pobrania nieistniejącego zasobu pod kod 404
        public RestResponse GetNonExistingUser()
        {
            var request = new RestRequest($"/users/9999999", Method.Get);
            return _client.Execute(request);
        }
    }
}
