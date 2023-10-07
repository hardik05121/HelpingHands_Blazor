using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXPaymentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int PaymentId{ get; set; }
    }
}
