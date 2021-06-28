using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DGA.RelyingParty.OpenID_Connect.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly HttpContext httpContext;
        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        //Local logout only
        //public async Task<IActionResult> OnGet()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    }

        //    return Redirect("/Index");
        //}

        //DGA Digital ID logout
        public IActionResult OnGet()
        {
            return SignOut("Cookies", OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
