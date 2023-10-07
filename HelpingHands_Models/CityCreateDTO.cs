using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CityCreateDTO
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("city name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string CityName { get; set; }

        [DisplayName("country name")]
        public int CountryId { get; set; }
        [DisplayName("state name")]
        [Required]
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
