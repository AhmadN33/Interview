using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Areas.Identity;
using WebApplication1.Models.CustomModels;
using WebApplication1.Models.services;

namespace WebApplication1.Pages
{
    public class LossTypeBase : ComponentBase
    {
        protected bool IsPageLoading { get; set; } = true;

        protected bool IsSearching { get; set; } = false;
        
        protected List<CustomLossTypes> Losstypes = new List<CustomLossTypes>();
        //protected List<LossType> Losstypes = new List<LossType>();
        protected bool IsShow { get; set; } = true;
        protected string Message { get; set; } = "";
        [Inject]
        protected NavigationManager navigation { get; set; }

        [Inject]
        public ILoginRepository loginservice { get; set; }

        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        ILogger<LossType> logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authstate = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authstate.User;

            
            if(!user.Identity.IsAuthenticated)
            {
                this.StateHasChanged();
                ((CustomAuthenticationStateProvider)authenticationStateProvider).Signout();
                navigation.NavigateTo("/");
                return;
            }
           

            IsPageLoading = true;
            StateHasChanged();
            try
            {
                Losstypes = (await loginservice.GetGetLossTypes()).ToList();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message, "Failed to load Loss Types");
                ((CustomAuthenticationStateProvider)authenticationStateProvider).Signout();
                navigation.NavigateTo("/Error");
            }
            
            IsPageLoading = false;
            this.StateHasChanged();
        }
    }
}
