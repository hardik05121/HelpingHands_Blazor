using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_Models.ViewModels
{
	public class CompanyXAmenityVM
	{
		public CompanyXAmenityVM()
		{
			CompanyXAmenity = new CompanyXAmenityCreateDTO();
			Company = new CompanyCreateDTO();
		}
		public CompanyXAmenityCreateDTO CompanyXAmenity { get; set; }
		public CompanyCreateDTO Company { get; set; }
		[ValidateNever]
		public List<CompanyXAmenityCreateDTO> CompanyXAmenitylist { get; set; }

		//      [ValidateNever]
		//public IEnumerable<SelectListItem> CarList { get; set; }
		[ValidateNever]
		public List<AmenityDTO> Amenitylist { get; set; }
	}
}
