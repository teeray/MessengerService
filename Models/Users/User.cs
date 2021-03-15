using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    public class User : HIDC
    {
        [Required]
        public string AuthID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Profile { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public Image Avatar { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
