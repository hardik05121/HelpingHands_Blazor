using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class ReviewUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public int CompanyID { get; set; }

        public string ApplicationUserId { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }



        [DisplayName("Like Count")]
        public int? LikeCount { get; set; }


        [DisplayName("Dis Like Count")]
        public int? DisLikeCount { get; set; }

        [DisplayName("View Count")]
        public int? ViewCount { get; set; }

        [DisplayName("Rating")]
        public int Rating { get; set; }

        [Required]
        [DisplayName("Review Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Review in Description")]
        public string Description { get; set; }

        public bool IsActive { get; set; }


    }
}
