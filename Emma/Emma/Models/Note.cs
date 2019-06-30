using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emma.Models
{
    public class Note
    {
        [Key]
        public int NoteNo { get; set; }

        [Required]
        public string NoteTitle { get; set; }

        [Required]
        public string NoteContents { get; set; }

        /// <summary>
        /// 동일하게 UserNo
        /// 
        /// </summary>
        [Required]
        public int UserNo { get; set; }
        /// <summary>
        /// 조인을 위한 foreignkey
        /// UserNO를 알려줘야 한다.
        /// Virtual은 lazy 로딩과 같다.
        /// </summary>
        [ForeignKey("UserNo")]
        public virtual User User { get; set; }
    }
}
