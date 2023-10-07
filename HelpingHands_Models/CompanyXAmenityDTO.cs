using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXAmenityDTO   
    {
        [Required]
        public int Id { get; set; }
        public int AmenityId { get; set; }
        [ValidateNever]
        public AmenityDTO Amenity { get; set; }
        public int CompanyId{ get; set; }
        [ValidateNever]
        public CompanyDTO Company { get; set; }
        
    }
}
