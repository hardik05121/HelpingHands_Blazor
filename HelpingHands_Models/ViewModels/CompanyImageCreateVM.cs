using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_Models.ViewModels
{
    public class CompanyImageCreateVM
    {
        public CompanyImageCreateVM()
        {
            CompanyImage = new CompanyImageCreateDTO();
            Company = new CompanyCreateDTO();
        }
        public CompanyImageCreateDTO CompanyImage { get; set; }

        public CompanyCreateDTO Company { get; set; }

        [ValidateNever]
        public List<CompanyImageCreateDTO> CompanyImagelist { get; set; }
        //[ValidateNever]
        //public List<CompanyImageDTO> CompanyImagelist { get; set; }
    }
}
