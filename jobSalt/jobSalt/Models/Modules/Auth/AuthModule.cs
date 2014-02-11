using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Modules.Auth
{
    public abstract class AuthModule
    {
        protected string name = "UNSET";

        public string UserID(string username)
        {
            return name + "_" + username;
        }

        public abstract bool IsValid(string _username, string _password);
    }
}