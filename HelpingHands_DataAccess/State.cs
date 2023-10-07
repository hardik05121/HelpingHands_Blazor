using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("State Name")]
        public string StateName { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; }

        public bool IsActive { get; set; }


    }
}
