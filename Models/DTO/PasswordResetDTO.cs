using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class PasswordResetDTO :LoginDTO
    {
        public string Code { get; set; }
    }
}
