using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class ThirdCategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
		[DisplayName("third category")]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string ThirdCategoryName { get; set; }

        [DisplayName("Third Category Image")]
        public string ThirdCategoryImage { get; set; }
        [Required]
        [DisplayName("first category")]
		public int FirstCategoryId { get; set; }
        [Required]
        [DisplayName("second category")]
		public int SecondCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
