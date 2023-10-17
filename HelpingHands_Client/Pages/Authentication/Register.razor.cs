using Azure;
using HelpingHands_Client.Helper;
using HelpingHands_Client.Service.IService;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace HelpingHands_Client.Pages.Authentication
{
    public partial class Register
    {
        private RegisterationRequestDTO RegisterationRequest = new RegisterationRequestDTO();
        public bool IsLoading { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthService _authSerivce { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsLoading = true;
            if (RegisterationRequest.Password != RegisterationRequest.ConfirmPassword)
            {
                await _jsRuntime.ToastrError("Password and confirm password are not match.");
                IsLoading = false;
                //Errors = result.Errors;
                //ShowRegistrationErrors = true;
            }
            var response = await _authSerivce.RegisterAsync<APIResponse>(RegisterationRequest);
            if (response != null && response.IsSuccess)
            {
                _navigationManager.NavigateTo("login");
            }
            else
            {
                if (response.ErrorMessages.Count > 0)
                {
                    //Errors = response.ErrorMessages;
                    //ShowRegistrationErrors = true;
                    await _jsRuntime.ToastrError(response.ErrorMessages.FirstOrDefault());
                }
            }
            IsLoading = false;

        }
    }
}
