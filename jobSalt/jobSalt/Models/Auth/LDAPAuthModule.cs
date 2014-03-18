using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Security.Permissions;

namespace jobSalt.Models.Auth
{
    public class LDAPAuthModule : AuthModule
    {
        private string connectionString;

        public LDAPAuthModule(string DomainController, string Name)
        {
            connectionString = DomainController;
            this.Name = Name;
        }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public override bool IsValid(string _username, string _password)
        {
            //TODO real connection string
            bool authenticated = false;
            //connection string in the form of subdomain.domain.tld
            string ldap = "LDAP://" + connectionString;

            try
            {
                //Setup user object
                DirectoryEntry entry = new DirectoryEntry(ldap, _username, _password);
                //Sign secure
                entry.AuthenticationType = AuthenticationTypes.Secure;
                //Retrive object, actually checks connections at this time
                object nativeObject = entry.NativeObject;

                authenticated = true;
            }
            catch (DirectoryServicesCOMException)
            {
                authenticated = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return authenticated;
        }
    }
}