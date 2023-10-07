using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string CityName { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; }
         [ForeignKey("State")]
        public int StateId { get; set; }
        [ValidateNever]
        public State State { get; set; }

        public bool IsActive { get; set; }


    }
}
