using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXServiceDTO
    {
        [Required]
        public int Id { get; set; }

        public int CompanyId { get; set; }
        [ValidateNever]
        public CompanyDTO Company { get; set; }

        public int ServiceId{ get; set; }
        [ValidateNever]
        public ServiceDTO Service { get; set; }


    }
}
