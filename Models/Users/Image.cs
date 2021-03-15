using Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    public class Image : HIDC
    {
        [Required]
        public string File { get; set; }
    }
}