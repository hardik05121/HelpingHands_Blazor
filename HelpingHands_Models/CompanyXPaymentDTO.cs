using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXPaymentDTO
    {
        [Required]
        
        public int Id { get; set; }


        public int CompanyId { get; set; }
        [ValidateNever]
        public CompanyDTO Company { get; set; }


        public int PaymentId{ get; set; }
        [ValidateNever]
        public PaymentDTO Payment{ get; set; }
    

    }
}
