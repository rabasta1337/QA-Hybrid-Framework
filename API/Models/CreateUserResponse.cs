using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.API.Models
{
    public class CreateUserResponse
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int Id { get; set; }
    }
}
