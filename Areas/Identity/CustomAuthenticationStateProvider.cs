using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Blazored.LocalStorage;
namespace WebApplication1.Areas.Identity
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
      
        private ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override  async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

           
            var Username = await _localStorageService.GetItemAsync<string>("DisplayName");

            ClaimsIdentity identity;

            if (Username !=null)
            {
                 identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,Username),

                    }, "apiauth_type");
            }
            else
            {
                 identity = new ClaimsIdentity();
            }


            var user = new ClaimsPrincipal(identity);

            return await  Task.FromResult(new AuthenticationState(user));
        }


        public Task<AuthenticationState> IsUserAuthenticated(bool IsAuthenticated ,string Username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,Username),

                }, "apiauth_type");

           
                var user = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

                return Task.FromResult(new AuthenticationState(user));
        }

        public void Signout()
        {
         
            _localStorageService.RemoveItemAsync("DisplayName");
            _localStorageService.RemoveItemAsync("UserLoginID");
         
           
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
