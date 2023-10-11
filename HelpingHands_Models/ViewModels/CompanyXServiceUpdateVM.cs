using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class CompanyXServiceUpdateVM
	{
        public CompanyXServiceUpdateVM()
        {
			CompanyXService = new CompanyXServiceUpdateDTO();
        }
        public CompanyXServiceUpdateDTO CompanyXService { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public List<ServiceDTO> ServiceList{ get; set; }

	}
}
