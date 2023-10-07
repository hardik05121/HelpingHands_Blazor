using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class ReviewXCommentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
 
        public int CompanyID { get; set; }

        public int ReviewID { get; set; }

        public string ApplicationUserId { get; set; }


        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DisplayName("Replay Comment")]
        public string Comment { get; set; }

    }
}
