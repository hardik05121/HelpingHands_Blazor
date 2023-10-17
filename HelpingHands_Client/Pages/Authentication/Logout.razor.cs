

using Blazored.LocalStorage;
using HelpingHands_Client.Serivce;
using HelpingHands_Client.Service.IService;
using HelpingHands_Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace HelpingHands_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthService _authSerivce { get; set; }
        [Inject]
        public HttpClient _client { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        [Inject]
        public AuthenticationStateProvider _authStateProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _localStorage.RemoveItemAsync(SD.SessionToken);
            await _localStorage.RemoveItemAsync(SD.UserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("/");
        }
           // await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
    }
}
