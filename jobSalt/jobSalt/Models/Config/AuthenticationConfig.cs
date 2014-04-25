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

        // Set defaults
        public AuthenticationConfig()
        {
            LDAPServers = new List<LDAPServer>();
        }
    }
}