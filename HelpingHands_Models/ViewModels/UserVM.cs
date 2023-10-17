using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            ApplicationUser = new ApplicationUserDTO();
            //RegisterationRequest = new RegisterationRequestDTO();
            ApplicationUserRole = new ApplicationUserRoleDTO();
        }
        public ApplicationUserDTO ApplicationUser { get; set; }
        public ApplicationUserRoleDTO ApplicationUserRole { get; set; }
        //public RegisterationRequestDTO RegisterationRequest { get; set; }

        [ValidateNever]
        public List<ApplicationUserRoleDTO> ApplicationUserRoleList { get; set; }

        [ValidateNever]
        public List<ApplicationRoleDTO> ApplicationRoleList { get; set; }
    }
}
