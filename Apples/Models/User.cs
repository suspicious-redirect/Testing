using System.ComponentModel.DataAnnotations;

namespace Apple.Models
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    public class User
    {
        [Key]
        public string UserName { get; set; }  
        public string Password { get; set; }
        public string? Address { get; set; }
    }
}
