using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGA.RelyingParty.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DGA.RelyingParty.OpenID_Connect.Pages
{
    public class TestModel : PageModel
    {
        public TestModel()
        {

        }

        public void OnGet(int times)
        {
            ViewData["Result"] = SecretUtility.encodeSecret("G7kI60eI2Vr", times);
        }
    }
}
