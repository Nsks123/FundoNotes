using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModels
{
    public class ForgotPasswordModel
    {
       
        public string Id { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
    }
    
}
