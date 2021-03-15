using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Models.ViewModel
{
    public class LoginViewModel
    {
        public IdentityUser User { get; set; }
        public string Token { get; set; }
    }
}
