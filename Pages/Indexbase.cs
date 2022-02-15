using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Areas.Identity;
using WebApplication1.Models;
using WebApplication1.Models.services;

namespace WebApplication1.Pages
{
    public class Indexbase : ComponentBase
    {
        protected bool IsPageLoading { get; set; } = true;

        protected bool IsSearching { get; set; } = false;
        protected User loginInformation = new User();
        protected bool IsShow { get; set; } = true;
        protected string Message { get; set; } = "";
        [Inject]
        protected NavigationManager navigation { get; set; }

        [Inject]
        public ILoginRepository loginservice { get; set; }

        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        public Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [Inject]
        ILogger<User> logger { get; set; }

        protected bool IsAuthenticated = false;
        protected override Task OnInitializedAsync()
        {
            IsPageLoading = true;
            StateHasChanged();
            IsPageLoading = false;
            this.StateHasChanged();
            return base.OnInitializedAsync();
        }

        protected async void ValidateUser() {


            IsSearching = true;
            StateHasChanged();

            User userReturnData = new User();

            try
            {
                userReturnData = await loginservice.GetUserByUserNameandPassowrd(loginInformation.UserName, loginInformation.Password);
            if (userReturnData != null)
            {
                IsAuthenticated = true;
                var authState = await ((CustomAuthenticationStateProvider)authenticationStateProvider).IsUserAuthenticated(IsAuthenticated, userReturnData.DisplayName);
                var user = authState.User;
                await localStorage.SetItemAsync("UserLoginID", userReturnData.UserId);
                await localStorage.SetItemAsync("DisplayName", userReturnData.DisplayName);
                navigation.NavigateTo("/ViewClaims");
                }
            else
            {
                IsAuthenticated = false;
                Message = "Your username or password is incorrect";
                IsShow = false;
                IsSearching = false;
            }
            StateHasChanged();
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex.Message, "Failed to validate credentials for UserName :" + loginInformation.UserName);
                ((CustomAuthenticationStateProvider)authenticationStateProvider).Signout();
                navigation.NavigateTo("/Error");
            }
        }
    }
}
