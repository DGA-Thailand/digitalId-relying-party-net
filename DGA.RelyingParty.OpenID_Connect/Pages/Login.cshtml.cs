using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DGA.RelyingParty.OpenID_Connect.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {
            string returnUrlQuery = "";
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrlQuery = $"?returnUrl={returnUrl}";
            }
            var auth = new AuthenticationProperties
            {
                RedirectUri = $"LoginCallBack{returnUrlQuery}"
            };

            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, auth);
        }
    }
}
