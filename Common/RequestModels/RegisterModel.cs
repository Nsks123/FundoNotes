using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModels
{
    public class RegisterModel
    {
        
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
