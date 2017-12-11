using System.ComponentModel.DataAnnotations;

namespace Stage2HW.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserNickName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
