﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Security.Permissions;
using System.Collections;

namespace jobSalt.Models.Auth
{
    public class LDAPAuthModule : AuthModule
    {
        private string connectionString;
        private string AdminGroup;

        public LDAPAuthModule(string DomainController, string AdminGroup, string Name)
        {
            connectionString = DomainController;
            this.AdminGroup = AdminGroup;
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
            var config = Config.ConfigLoader.AuthenticationConfig;

            if (config.RestrictAccessToUsers.Count > 0)
                if (!config.RestrictAccessToUsers.Contains(_username))
                    return false;

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


        public override bool IsAdmin(string _username)
        {
            if (Config.ConfigLoader.AuthenticationConfig.AdminUsers.Contains(_username))
                return true;
            return false;

            /*
            ArrayList groupMemberships = new ArrayList();
            groupMemberships = AttributeValuesMultiString("memberOf", _username,
                groupMemberships, true);

            return groupMemberships.Contains(AdminGroup);*/
        }

        private ArrayList AttributeValuesMultiString(string attributeName, string objectDn, ArrayList valuesCollection, bool recursive)
        {
            DirectoryEntry ent = new DirectoryEntry(objectDn);
            PropertyValueCollection ValueCollection = ent.Properties[attributeName];
            IEnumerator en = ValueCollection.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current != null)
                {
                    if (!valuesCollection.Contains(en.Current.ToString()))
                    {
                        valuesCollection.Add(en.Current.ToString());
                        if (recursive)
                        {
                            AttributeValuesMultiString(attributeName, "LDAP://" +
                            en.Current.ToString(), valuesCollection, true);
                        }
                    }
                }
            }
            ent.Close();
            ent.Dispose();
            return valuesCollection;
        }
    }
}