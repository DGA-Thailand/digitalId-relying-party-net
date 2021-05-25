using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGA.RelyingParty.OpenID_Connect.MVC.Models
{
    public class Profile
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string CitizenId { get; set; }
        public bool CitizenIdVerified { get; set; }

        public string Email { get; set; }
        public bool EmailVerified { get; set; }

        public string Mobile { get; set; }
        public bool MobileVerified { get; set; }

        public double IALLevel { get; set; }
    }
}
