using Models.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Messengers
{
    public class Message : HIDC
    {
        public int PoolID { get; set; }
        [IgnoreDataMember]
        public Pool Pool { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string FromAuth { get; set; }
    }
}