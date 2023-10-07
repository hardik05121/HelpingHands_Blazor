using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class SecondCategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Second Category")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string SecondCategoryName { get; set; }
        [DisplayName("Second Category Image")]
        public string SecondCategoryImage { get; set; }
		[DisplayName("first category")]
		[Required]

		public int FirstCategoryId { get; set; }


        public bool IsActive { get; set; }


    }
}
