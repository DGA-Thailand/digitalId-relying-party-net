using DGA.RelyingParty.OpenID_Connect.Models;
using DGA.RelyingParty.OpenID_Connect.Pages;
using DGA.RelyingParty.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DGA.RelyingParty.OpenID_Connect.Utilities
{
    public class SignInUtility
    {
        public Profile GetProfile(ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var obj = new Profile();
                foreach (var claim in principal.Claims)
                {
                    if (claim.Type == DGAScope.UserId)
                        obj.UserId = claim.Value;
                    else if (claim.Type == DGAScope.Email)
                        obj.Email = claim.Value;
                    else if (claim.Type == DGAScope.FirstName)
                        obj.FirstName = claim.Value;
                    else if (claim.Type == DGAScope.LastName)
                        obj.LastName = claim.Value;
                    else if (claim.Type == DGAScope.MiddleName)
                        obj.MiddleName = claim.Value;
                    else if (claim.Type == DGAScope.CitizenId)
                        obj.CitizenId = claim.Value;
                    else if (claim.Type == DGAScope.IALLevel)
                        obj.IALLevel = Convert.ToDouble(claim.Value);
                    else if (claim.Type == DGAScope.Mobile)
                        obj.Mobile = claim.Value;
                    else if (claim.Type == DGAScope.UserName)
                        obj.UserName = claim.Value;
                    else if (claim.Type == DGAScope.CitizenIdVerified)
                        obj.CitizenIdVerified = Convert.ToBoolean(claim.Value);
                    else if (claim.Type == DGAScope.EmailVerified)
                        obj.EmailVerified = Convert.ToBoolean(claim.Value);
                    else if (claim.Type == DGAScope.MobileVerified)
                        obj.MobileVerified = Convert.ToBoolean(claim.Value);
                }

                return obj;
            }

            return null;
        }
    }
}
