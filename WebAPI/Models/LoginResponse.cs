using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class LoginResponse
    {
        public string userName { get; set; }

        public string Token { get; set; }
    }
}