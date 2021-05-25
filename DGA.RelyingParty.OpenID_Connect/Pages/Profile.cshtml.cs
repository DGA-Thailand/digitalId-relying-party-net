using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGA.RelyingParty.OpenID_Connect.Models;
using DGA.RelyingParty.OpenID_Connect.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DGA.RelyingParty.OpenID_Connect.Pages
{
    public class ProfileModel : PageModel
    {
        public Profile UserProfile { get; set; }

        private readonly SignInUtility signInUtility;
        public ProfileModel(SignInUtility signInUtility)
        {
            this.signInUtility = signInUtility;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserProfile = signInUtility.GetProfile(User);
            }

            //return Page();
        }
    }
}
