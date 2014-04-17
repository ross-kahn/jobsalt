using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Auth
{
    public abstract class AuthModule
    {
        public string Name { get; protected set; }

        public string UserID(string username)
        {
            return Name + "_" + username;
        }

        public abstract bool IsValid(string _username, string _password);

        public abstract bool IsAdmin(string _username);
    }
}