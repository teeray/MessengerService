using Models.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Messengers
{
    public class Member : HIDC
    {
        public int PoolID { get; set; }
        [IgnoreDataMember]
        public Pool Pool { get; set; }
        [Required]
        public string AuthID { get; set; }
    }
}