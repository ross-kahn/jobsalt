using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Config
{
    public class LDAPServer
    {
        public string Name { get; set; }
        public string DomainController { get; set; }
        public string AdminGroup { get; set; }
    }
    public class AuthenticationConfig
    {
        public List<LDAPServer> LDAPServers { get; set; }

        public List<string> AdminUsers { get; set; }
        public List<string> RestrictAccessToUsers { get; set; }

        // Set defaults
        public AuthenticationConfig()
        {
            LDAPServers = new List<LDAPServer>();

            AdminUsers = new List<string>();
            RestrictAccessToUsers = new List<string>();
        }
    }
}