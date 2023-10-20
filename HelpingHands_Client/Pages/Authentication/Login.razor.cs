using Azure;
using HelpingHands_Client.Service.IService;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using HelpingHands_Models;
using HelpingHands_Common;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using HelpingHands_Client.Helper;
using NuGet.Protocol.Plugins;
using HelpingHands_Client.Serivce;

namespace HelpingHands_Client.Pages.Authentication
{
    public partial class Login
    {
        private readonly HttpClient _client;
        //private readonly ILocalStorageService _localStorage;
        private LoginRequestDTO LoginRequest = new LoginRequestDTO();
        private LoginResponseDTO LoginResponse = new LoginResponseDTO();
        public bool IsLoading { get; set; }


        [Inject]
        public IAuthService _authSerivce { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }
        [Inject]
        public AuthenticationStateProvider _authStateProvider { get; set; }

        private async Task LoginUser()
        {
            APIResponse response = await _authSerivce.LoginAsync<APIResponse>(LoginRequest);
            if (response != null && response.IsSuccess)
            {
                LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                if (!string.IsNullOrEmpty(model?.Token))
                {
                    await _localStorage.SetItemAsync(SD.SessionToken, model.Token);
                    await _localStorage.SetItemAsync(SD.UserDetails, model.User);
                    //await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SD.SessionToken, model.Token);
                    //await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SD.UserDetails, model.User);

                    var identity = new ClaimsIdentity(new[]
                    {
                     new Claim(ClaimTypes.Name, new JwtSecurityToken(model.Token).Claims.FirstOrDefault(u => u.Type == "unique_name")?.Value),
                     new Claim(ClaimTypes.Role, new JwtSecurityToken(model.Token).Claims.FirstOrDefault(u => u.Type == "role")?.Value),
                     new Claim(ClaimTypes.NameIdentifier, new JwtSecurityToken(model.Token).Claims.FirstOrDefault(u => u.Type == "nameid")?.Value),
                    }, "apiauth");

                    var user = new ClaimsPrincipal(identity);
                    var authState = new AuthenticationState(user);
                   //((AuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged(Task.FromResult(authState));

                    _navigationManager.NavigateTo("/",forceLoad:true);
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        await _jsRuntime.ToastrError(response.ErrorMessages.FirstOrDefault());
                    }
                    StateHasChanged(); // Notify Blazor to re-render
                }
            }
        }
        protected override async Task OnInitializedAsync()
        {
            IsLoading = false;
        }
    }
}
