using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_DataAccess
{
    public class CompanyXService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("Service")]
        public int ServiceId{ get; set; }
        [ValidateNever]
        public Service Service { get; set; }


    }
}
