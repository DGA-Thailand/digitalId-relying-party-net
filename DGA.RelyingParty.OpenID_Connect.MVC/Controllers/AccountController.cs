using DGA.RelyingParty.OpenID_Connect.MVC.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGA.RelyingParty.OpenID_Connect.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpContext httpContext;
        private readonly SignInUtility signInUtility;
        public AccountController(IHttpContextAccessor httpContextAccessor, SignInUtility signInUtility)
        {
            httpContext = httpContextAccessor.HttpContext;
            this.signInUtility = signInUtility;
        }

        public IActionResult Login(string returnUrl = null)
        {
            var auth = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallBack", "Account", new {returnUrl = returnUrl })
            };

            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, auth);
        }

        public IActionResult LoginCallBack(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return new RedirectResult(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest();


            return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "~/" : returnUrl);
        }

        [Authorize]
        public IActionResult Profile()
        {            
            var profile = signInUtility.GetProfile(User);
            return View(profile);
        }

        //Local logout only
        //public async Task<IActionResult> Logout()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        //DGA Digital ID logout
        public IActionResult Logout()
        {
            return SignOut("Cookies", OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult RemoteFailure()
        {
            return View();
        }
    }
}
