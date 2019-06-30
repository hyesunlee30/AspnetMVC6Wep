using System.ComponentModel.DataAnnotations;

namespace Emma.Models
{
    public class User
    {
        [Key]
        public int UserNo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserPassqord { get; set; }
    }
}
