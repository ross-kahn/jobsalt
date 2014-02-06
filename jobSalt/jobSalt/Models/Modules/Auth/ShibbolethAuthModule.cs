using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml2;

namespace jobSalt.Models.Modules.Auth
{
    public class ShibbolethAuthModule : AuthModule
    {
        public override bool IsValid(string _username, string _password)
        {
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor();
            descriptor.TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";
            DateTime issueInstant = DateTime.UtcNow;
            descriptor.Lifetime = new Lifetime(issueInstant, issueInstant + new TimeSpan(8, 0, 0));
            descriptor.AppliesToAddress = "http://localhost/";

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("urn:oasis:names:tc:SAML:1.0:am:password", _password));
            claims.Add(new Claim("User", _username));
            claims.Add(new Claim("Permission", "Read"));
            claims.Add(new Claim("Permission", "Write"));
            claims.Add(new Claim("Permission", "Update"));
            claims.Add(new Claim("Permission", "Delete"));
            descriptor.Subject = new ClaimsIdentity(claims);

            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = store.Certificates.Find(X509FindType.FindBySubjectName, "shibboleth.ad.kasour.com", true);

            X509Certificate2 signingCert = collection[0];
            SecurityKeyIdentifier ski = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[] { new X509SecurityToken(signingCert).CreateKeyIdentifierClause<X509SubjectKeyIdentifierClause>() });
            X509SigningCredentials signingCreds = new X509SigningCredentials(signingCert, ski);
            descriptor.SigningCredentials = signingCreds;

            Saml2SecurityTokenHandler tokenHandler = new Saml2SecurityTokenHandler();
            Saml2SecurityToken token = tokenHandler.CreateToken(descriptor) as Saml2SecurityToken;

            string path = "saml2bearer.xml";
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlDictionaryWriter xmlwriter = XmlDictionaryWriter.CreateTextWriter(fs);
            tokenHandler.WriteToken(xmlwriter, token);
            xmlwriter.Flush();
            xmlwriter.Close();
            fs.Dispose();

            X509Store encryptedStore = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            encryptedStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection encryptedCollection = store.Certificates.Find(X509FindType.FindBySubjectName, "shibboleth.ad.kasour.com", true);

            X509Certificate2 encryptingCert = encryptedCollection[0];
            SecurityKeyIdentifier encryptingSki = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[] { new X509SecurityToken(encryptingCert).CreateKeyIdentifierClause<X509SubjectKeyIdentifierClause>() });
            X509AsymmetricSecurityKey encryptingKey = new X509AsymmetricSecurityKey(encryptingCert);
            EncryptingCredentials encryptingCreds = new EncryptingCredentials(encryptingKey, encryptingSki, "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
            descriptor.EncryptingCredentials = encryptingCreds;

            return true;
        }
    }
}