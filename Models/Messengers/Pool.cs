using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Messengers
{
    public class Pool : HIDC
    {
        public List<Member> Members { get; set; }
        public List<Message> Messages { get; set; }
    }
}
