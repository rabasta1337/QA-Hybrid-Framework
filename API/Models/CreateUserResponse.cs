using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.API.Models
{
    // Model DTO (Data Transfer Object) reprezentujący strukturę odpowiedzi JSON 
    public class CreateUserResponse
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int Id { get; set; }
    }
}
