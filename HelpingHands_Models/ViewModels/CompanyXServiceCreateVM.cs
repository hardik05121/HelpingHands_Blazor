using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class CompanyXServiceCreateVM
	{
        public CompanyXServiceCreateVM()
        {
			CompanyXService = new CompanyXServiceCreateDTO();
        }
        public CompanyXServiceCreateDTO CompanyXService { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public List<ServiceDTO> ServiceList{ get; set; }

	}
}
