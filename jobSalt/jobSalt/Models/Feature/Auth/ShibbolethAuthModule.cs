using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace jobSalt.Models.Feature.Auth
{
    public class ShibbolethAuthModule : AuthModule
    {
        private string issuer;
        private string recipient;
        private string domain;
        private string subject;

        public ShibbolethAuthModule(string issuer, string recipient, string domain, string subject)
        {
            this.issuer = issuer;
            this.recipient = recipient;
            this.domain = domain;
            this.subject = subject;
        }

        public override bool IsValid(string _username, string _password)
        {
            Dictionary<string, string> attributes = new Dictionary<string,string>();
            //attributes.add("Principle", "P");

            string saml = SamlHelper.GetPostSamlResponse( recipient
                                                        , issuer
                                                        , domain
                                                        , subject
                                                        , StoreLocation.LocalMachine
                                                        , StoreName.Root
                                                        , X509FindType.FindByThumbprint
                                                        , null
                                                        , null
                                                        , "41fe9204effd0d8c5e65a1de3a507da1383fd14f"
                                                        , attributes);

            return true;
        }
    }
}