using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class CompanyXServiceVM
	{
        public CompanyXServiceVM()
        {
            CompanyXService = new CompanyXServiceCreateDTO();
            Company = new CompanyCreateDTO();
        }
        public CompanyXServiceCreateDTO CompanyXService { get; set; }

        public CompanyCreateDTO Company { get; set; }

        [ValidateNever]
        public List<CompanyXServiceCreateDTO> CompanyXServicelist { get; set; }

        //      [ValidateNever]
        //public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public List<ServiceDTO> ServiceList { get; set; }

    }
}
