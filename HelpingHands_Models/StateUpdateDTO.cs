using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class StateUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("state name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string StateName { get; set; }
		[Required]
		[DisplayName("country name")]

		public int CountryId { get; set; }

        public bool IsActive { get; set; }


    }
}
