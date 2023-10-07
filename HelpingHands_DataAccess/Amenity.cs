using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class Amenity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Amenity Name")]
        public string AmenityName { get; set; }

        [ForeignKey("FirstCategory")]
        public int FirstCategoryId { get; set; }
        [ValidateNever]
        public FirstCategory FirstCategory { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
		public bool IsCheked { get; set; }

	}
}
