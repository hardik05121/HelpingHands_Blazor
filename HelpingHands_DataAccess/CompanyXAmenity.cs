using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_DataAccess
{
    public class CompanyXAmenity   
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Amenity")]
        public int AmenityId { get; set; }
        [ValidateNever]
        public Amenity Amenity { get; set; }

        [ForeignKey("Company")]
        public int CompanyId{ get; set; }
        [ValidateNever]
        public Company Company { get; set; }
        
    }
}
