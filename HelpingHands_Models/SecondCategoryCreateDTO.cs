using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class SecondCategoryCreateDTO
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("second category")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 50 characters.")]
        public string SecondCategoryName { get; set; }
        [DisplayName("Second Category Image")]
        public string SecondCategoryImage { get; set; }
        [DisplayName("first category")]
        [Required]
        public int FirstCategoryId { get; set; }

        public bool IsActive { get; set; }


    }
}
