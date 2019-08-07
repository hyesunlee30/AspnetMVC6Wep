using System.ComponentModel.DataAnnotations;

namespace Emma.Models
{
    public class User
    {
        [Key]
        public int UserNo { get; set; }

        [Required(ErrorMessage ="사용자 이름을 입력하세요.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "사용자 아이디를 입력하세요.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public string UserPassword { get; set; }
    }
}
